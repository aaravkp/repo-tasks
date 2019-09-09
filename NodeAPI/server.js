var express = require('express');
var app = express();
var fs = require("fs");

app.get('/api/tariff/:cosumption', function (req, res) {
    let tariff = new Array();

    let cons = Number(req.params.cosumption);
    let basicCost =0;
    let packagedCost = 0;
    if (cons > 0)
    {
        basicCost = (5 * 12) + (cons * 0.22);
        record1 = {tariffName:"Basic electricity tariff", annualCost:basicCost};
        if (cons <= 4000)
        {
            packagedCost = 800;
        }
        else
        {
            packagedCost = 800 + ((cons - 4000) * 0.30);
        }
        record2 = {tariffName:"Packaged tariff", annualCost:packagedCost};
        tariff.push(record1, record2);

        tariff.sort((a, b) => parseFloat(a.annualCost) - parseFloat(b.annualCost));

    }
    else
    {
        tariff = null;
    }

    res.send(JSON.stringify(tariff))
});

var server = app.listen(8081, function () {
   var host = server.address().address
   var port = server.address().port
   console.log("Example app listening at http://%s:%s", host, port)
})