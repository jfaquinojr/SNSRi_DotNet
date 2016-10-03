var app = angular.module("app");

app.factory("deviceService", deviceService);

function deviceService($http) {

    
    var svc = $http;
    var self = this;
    self.urlHomeSeer = "http://localhost:8002";

    return {
        getHomeSeerDevice: getHomeSeerDevice,
        setHomeSeerDevice: setHomeSeerDevice,
        urlHomeSeer: self.urlHomeSeer
    }

    function getHomeSeerDevice(refId) {
        var url = self.urlHomeSeer + "/JSON?request=getstatus&ref=" + refId;
        //console.log(url);
        return svc.get(url);
    }

    function setHomeSeerDevice() {
        
    }

    //function query(params) {
    //    var sdata;
    //    return svc.get($.getJSON(params.url + "/JSON?request=getstatus&ref=" + params.ref))
    //        .then(function(data) {
    //            sdata = data;
    //            return $.getJSON(params.url + "/JSON?request=getcontrol&ref=" + params.ref);
    //        })
    //        .then(function(cdata) {
    //            return cdata;
    //        })
    //        .catch(function(e) {
    //            console.log(params.ref + " device is not controllable");
    //            return "";
    //        })
    //        .then(function(cdata) {
    //            return new Device($.extend(cdata, sdata));
    //        });
    //}
}


/*
example data returned by 
/JSON?request=getstatus&ref=8:

{
  "Name": "HomeSeer Devices",
  "Version": "1.0",
  "Devices": [
    {
      "ref": 8,
      "name": "Device 08",
      "location": "Room 102",
      "location2": "1F",
      "value": 0,
      "status": "Off",
      "device_type_string": "",
      "last_change": "/Date(1474887846569)/",
      "relationship": 0,
      "hide_from_view": false,
      "associated_devices": [],
      "device_type": {
        "Device_API": 0,
        "Device_API_Description": "No API",
        "Device_Type": 4,
        "Device_Type_Description": "Type 4",
        "Device_SubType": 0,
        "Device_SubType_Description": ""
      },
      "device_image": "",
      "UserNote": "",
      "UserAccess": "Any",
      "status_image": "/images/HomeSeer/status/off.gif"
    }
  ]
}



example data returned by
/JSON?request=getcontrol&ref=8

{
  "ControlPairs": [
    {
      "Do_Update": true,
      "SingleRangeEntry": true,
      "ControlButtonType": 0,
      "ControlButtonCustom": "",
      "CCIndex": 0,
      "Range": null,
      "Ref": 8,
      "Label": "Off",
      "ControlType": 5,
      "ControlLocation": {
        "Row": 0,
        "Column": 0,
        "ColumnSpan": 0
      },
      "ControlLoc_Row": 0,
      "ControlLoc_Column": 0,
      "ControlLoc_ColumnSpan": 0,
      "ControlUse": 2,
      "ControlValue": 0,
      "ControlString": "",
      "ControlStringList": null,
      "ControlStringSelected": null,
      "ControlFlag": false
    },
    {
      "Do_Update": true,
      "SingleRangeEntry": true,
      "ControlButtonType": 0,
      "ControlButtonCustom": "",
      "CCIndex": 1,
      "Range": null,
      "Ref": 8,
      "Label": "On",
      "ControlType": 5,
      "ControlLocation": {
        "Row": 0,
        "Column": 0,
        "ColumnSpan": 0
      },
      "ControlLoc_Row": 0,
      "ControlLoc_Column": 0,
      "ControlLoc_ColumnSpan": 0,
      "ControlUse": 1,
      "ControlValue": 100,
      "ControlString": "",
      "ControlStringList": null,
      "ControlStringSelected": null,
      "ControlFlag": false
    }
  ],
  "ref": 8,
  "name": "Device 08",
  "location": "Room 102",
  "location2": "1F"
}
*/