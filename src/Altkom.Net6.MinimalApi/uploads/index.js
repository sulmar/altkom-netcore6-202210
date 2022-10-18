const express = require("express");
const fileUpload = require("express-fileupload");
const path = require("path");
const SaxonJS = require("saxon-js");
const date = require("date-and-time");
const { exec } = require("child_process");
const morgan = require("morgan");

const app = express();
const PORT = process.env.PORT || 3000;

const stylesheetFileName = "AGC.sef.json";

app.use(express.static(path.join(__dirname, "public")));

// enable files upload
app.use(fileUpload());

app.use(morgan("tiny"));

app.put("/xslt3", (req, res) => {
  exec(
    `xslt3 -xsl:AGC.xslt -export:${stylesheetFileName} -nogo -t -ns:##html5`,
    (error, stdout, stderr) => {
      if (error) {
        console.log(`error: ${error.message}`);
        console.log(error.code);
        return;
      }
      if (stderr) {
        console.log(`stderr: ${stderr}`);
        // return;
      }

      console.log(`stdout: ${stdout}`);

      res.send(stylesheetFileName + " updated.");
    }
  );
});

// https://www.saxonica.com/saxon-js/documentation2/index.html#!api/transform

app.post("/orders", (req, res) => {
  if (!req.files || Object.keys(req.files).length === 0) {
    return res.status(400).send("No file uploaded");
  }

  console.log(req.files.xmlFile.name); // the uploaded xml file object

  const filename = req.files.xmlFile.name;
  const regexp = /^(?<orderNumber>\d{2}_\d{4}_[A-Z]*)/g;
  const result = filename.match(regexp);

  if (!result) {
    return res
      .status(400)
      .send(`Invalid filename. Valid format is RR_MMDD_NrZamowienia_...`);
  }

  const supplier = "AGC";
  const orderNumber = result[0];
  const deliveryDate = date.format(new Date(), "DD.MM.YYYY");

  var options = {
    stylesheetFileName: stylesheetFileName,
    sourceText: req.files.xmlFile.data.toString(),
    destination: "serialized",
    stylesheetParams: {
      "order-number": orderNumber,
      supplier: supplier,
      "delivery-date": deliveryDate,
    },
  };

  SaxonJS.transform(options, "async")
    .then((result) => {
      console.log(result.principalResult);
      res.set("Content-Type", "application/octet-stream");
      res.set("Content-Disposition", `attachment; filename=${orderNumber}.csv`);
      res.send(result.principalResult);
    })
    .catch((error) => console.log(error));
});

app.get("/", (req, res) => {
  res.sendFile("index.html");
});

app.listen(PORT, () => {
  console.log(`Server is listening on port ${PORT}`);
});
