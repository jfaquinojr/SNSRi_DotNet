using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SNSRi.Api.Tests.Helpers;
using SNSRi.Business;
using SNSRi.Entities;
using SNSRi.Entities.HomeSeer;

namespace SNSRi.Api.Tests.Fakes
{
    internal class FakeHttpClient : IHttpClient
    {
        public string GetStringAsync(string requestUri)
        {
            if (requestUri.Contains("brhumidity"))
                return GetJsonStatusBRHumidity();

            return GetJson();
        }

        private string GetJson()
        {
            return @"
            {
                'Name': 'HomeSeer Devices',
                'Version': '1.0',
                'Devices': [
                  {
                      'ref': 9,
                      'name': 'Another Device',
                      'location': 'Room 01',
                      'location2': 'Floor 01',
                      'value': 0,
                      'status': 'Off',
                      'device_type_string': '',
                      'last_change': '/Date(-62135596800000)/',
                      'relationship': 0,
                      'hide_from_view': false,
                      'associated_devices': [],
                      'device_type': {
                          'Device_API': 0,
                          'Device_API_Description': 'No API',
                          'Device_Type': 0,
                          'Device_Type_Description': 'Type 0',
                          'Device_SubType': 0,
                          'Device_SubType_Description': ''
                      },
                      'device_image': '',
                      'UserNote': '',
                      'UserAccess': 'Any',
                      'status_image': '/images/HomeSeer/status/off.gif'
                  },
                  {
                      'ref': 6,
                      'name': 'Device 1.1',
                      'location': 'Room 01',
                      'location2': 'Floor 01',
                      'value': 0,
                      'status': 'Off',
                      'device_type_string': '',
                      'last_change': '/Date(1485958344947)/',
                      'relationship': 0,
                      'hide_from_view': false,
                      'associated_devices': [],
                      'device_type': {
                          'Device_API': 0,
                          'Device_API_Description': 'No API',
                          'Device_Type': 0,
                          'Device_Type_Description': 'Type 0',
                          'Device_SubType': 0,
                          'Device_SubType_Description': ''
                      },
                      'device_image': '',
                      'UserNote': '',
                      'UserAccess': 'Any',
                      'status_image': '/images/HomeSeer/status/off.gif'
                  },
                  {
                      'ref': 8,
                      'name': 'Device Latest',
                      'location': 'Room 01',
                      'location2': 'Floor 01',
                      'value': 0,
                      'status': 'Off',
                      'device_type_string': '',
                      'last_change': '/Date(-62135596800000)/',
                      'relationship': 0,
                      'hide_from_view': false,
                      'associated_devices': [],
                      'device_type': {
                          'Device_API': 0,
                          'Device_API_Description': 'No API',
                          'Device_Type': 0,
                          'Device_Type_Description': 'Type 0',
                          'Device_SubType': 0,
                          'Device_SubType_Description': ''
                      },
                      'device_image': '',
                      'UserNote': '',
                      'UserAccess': 'Any',
                      'status_image': '/images/HomeSeer/status/off.gif'
                  },
                  {
                      'ref': 7,
                      'name': 'Device 1.2',
                      'location': 'Unknown',
                      'location2': 'Floor 01',
                      'value': 100,
                      'status': 'On',
                      'device_type_string': 'JOJODEVICETYPE',
                      'last_change': '/Date(1485959449763)/',
                      'relationship': 0,
                      'hide_from_view': false,
                      'associated_devices': [],
                      'device_type': {
                          'Device_API': 0,
                          'Device_API_Description': 'No API',
                          'Device_Type': 0,
                          'Device_Type_Description': 'Type 0',
                          'Device_SubType': 0,
                          'Device_SubType_Description': ''
                      },
                      'device_image': '',
                      'UserNote': '',
                      'UserAccess': 'Any',
                      'status_image': '/images/HomeSeer/status/on.gif'
                  },
                  {
                      'ref': 5,
                      'name': ' 1234',
                      'location': 'Unknown',
                      'location2': 'Unknown',
                      'value': 100,
                      'status': 'On',
                      'device_type_string': '',
                      'last_change': '/Date(1485781006398)/',
                      'relationship': 0,
                      'hide_from_view': false,
                      'associated_devices': [],
                      'device_type': {
                          'Device_API': 0,
                          'Device_API_Description': 'No API',
                          'Device_Type': 0,
                          'Device_Type_Description': 'Type 0',
                          'Device_SubType': 0,
                          'Device_SubType_Description': ''
                      },
                      'device_image': '',
                      'UserNote': '',
                      'UserAccess': 'Any',
                      'status_image': '/images/HomeSeer/status/on.gif'
                  },
                  {
                      'ref': 10,
                      'name': '12:16AM Device',
                      'location': 'Unknown',
                      'location2': 'Unknown',
                      'value': 0,
                      'status': 'Off',
                      'device_type_string': '',
                      'last_change': '/Date(-62135596800000)/',
                      'relationship': 0,
                      'hide_from_view': false,
                      'associated_devices': [],
                      'device_type': {
                          'Device_API': 0,
                          'Device_API_Description': 'No API',
                          'Device_Type': 0,
                          'Device_Type_Description': 'Type 0',
                          'Device_SubType': 0,
                          'Device_SubType_Description': ''
                      },
                      'device_image': '',
                      'UserNote': '',
                      'UserAccess': 'Any',
                      'status_image': '/images/HomeSeer/status/off.gif'
                  },
                  {
                      'ref': 3,
                      'name': 'New Device',
                      'location': 'Unknown',
                      'location2': 'Unknown',
                      'value': 100,
                      'status': 'On',
                      'device_type_string': '',
                      'last_change': '/Date(1485959988134)/',
                      'relationship': 0,
                      'hide_from_view': false,
                      'associated_devices': [],
                      'device_type': {
                          'Device_API': 0,
                          'Device_API_Description': 'No API',
                          'Device_Type': 0,
                          'Device_Type_Description': 'Type 0',
                          'Device_SubType': 0,
                          'Device_SubType_Description': ''
                      },
                      'device_image': '',
                      'UserNote': '',
                      'UserAccess': 'Any',
                      'status_image': '/images/HomeSeer/status/on.gif'
                  },
                  {
                      'ref': 4,
                      'name': 'Oldest Device',
                      'location': 'Unknown',
                      'location2': 'Unknown',
                      'value': 100,
                      'status': 'On',
                      'device_type_string': '',
                      'last_change': '/Date(1485959989046)/',
                      'relationship': 0,
                      'hide_from_view': false,
                      'associated_devices': [],
                      'device_type': {
                          'Device_API': 0,
                          'Device_API_Description': 'No API',
                          'Device_Type': 0,
                          'Device_Type_Description': 'Type 0',
                          'Device_SubType': 0,
                          'Device_SubType_Description': ''
                      },
                      'device_image': '',
                      'UserNote': '',
                      'UserAccess': 'Any',
                      'status_image': '/images/HomeSeer/status/on.gif'
                  }
                ]
            }
            ";
        }

        private string GetJsonStatusBRHumidity()
        {
            return @"
            {
              'Name': 'HomeSeer Devices',
              'Version': '1.0',
              'Devices': [
                {
                  'ref': 43,
                  'name': 'BR Humidity',
                  'location': 'Z-Net #1',
                  'location2': '101',
                  'value': 53,
                  'status': '53 %',
                  'device_type_string': 'Z-Wave Relative Humidity',
                  'last_change': '/Date(1490115898906)/',
                  'relationship': 4,
                  'hide_from_view': false,
                  'associated_devices': [
                    37
                  ],
                  'device_type': {
                    'Device_API': 4,
                    'Device_API_Description': 'Plug-In API',
                    'Device_Type': 0,
                    'Device_Type_Description': 'Plug-In Type 0',
                    'Device_SubType': 5,
                    'Device_SubType_Description': 'Relative Humidity'
                  },
                  'device_image': '',
                  'UserNote': '',
                  'UserAccess': 'Any',
                  'status_image': '/images/HomeSeer/status/water.gif'
                }
              ]
            }
            ";
        }
    }
}
