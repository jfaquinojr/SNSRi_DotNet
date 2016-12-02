//declare var HomeSeerUrl: string;

//module App {

    

//    import Device = Data.Contracts.Device;

//    export interface IDeviceService {
//        getHomeSeerDevice(refId: number): Device;
//        setHomeSeerDevice(device: Device): void;
//    }

//    class DeviceService implements IDeviceService {

//        private homeSeerUrl: string;

//        static $inject = ["$http"];

//        constructor(private $http: ng.IHttpService) {
//            this.$http.get("/api/Config/HomeSeerUrl")
//                .then((url: string) => {
//                    this.homeSeerUrl = url;
//                });
//        }

//        //query(params: ng.): ng.IHttpPromise<Device> {
//        //    const self = this;
//        //    var sdata;
//        //    return self.$http.get($.getJSON(params.url + "/JSON?request=getstatus&ref=" + params.ref))
//        //        .then(function(data) {
//        //            sdata = data;
//        //            return $.getJSON(params.url + "/JSON?request=getcontrol&ref=" + params.ref);
//        //        })
//        //        .then(function(cdata) {
//        //            return cdata;
//        //        })
//        //        .catch(function(e) {
//        //            console.log(params.ref + " device is not controllable");
//        //            return "";
//        //        })
//        //        .then(function(cdata) {
//        //            return new Device($.extend(cdata, sdata));
//        //        });
//        //}

//        getHomeSeerDevice(refId: number): Device {
//            const self = this;
//            const url = self.homeSeerUrl + "/JSON?request=getstatus&ref=" + refId;
//            var dev: Device;
//            let device: Device;
//            self.$http.get(url)
//                .then(response => {
//                    console.log(JSON.stringify(response.data));
//                    dev.Value = "OK LANG!~!!";
//                });

//            return dev;
//        }

//        setHomeSeerDevice(device: Device): void {
//            // wala pa masyadong docs
//        }
//    }
//}

//var app = angular.module("app");

//app.factory("deviceService", deviceService);

//function deviceService($http) {

    
//    var svc = $http;
//    var self = this;
//    self.urlHomeSeer = HomeSeerUrl || "http://localhost:8002";

//    return {
//        getHomeSeerDevice: getHomeSeerDevice,
//        setHomeSeerDevice: setHomeSeerDevice,
//        urlHomeSeer: self.urlHomeSeer
//    }

//    function getHomeSeerDevice(refId) {
//        var url = self.urlHomeSeer + "/JSON?request=getstatus&ref=" + refId;
//        //console.log(url);
//        return svc.get(url);
//    }

//    function setHomeSeerDevice() {
        
//    }


//}


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