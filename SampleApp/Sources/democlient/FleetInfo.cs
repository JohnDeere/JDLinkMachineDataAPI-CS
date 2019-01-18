using System.IO;
using System.Collections.Generic;
using SampleApp.Sources.democlient.rest;
using System.Net.Http;
using System.IO.Compression;
using System.Xml.Serialization;
using System.Net;

namespace SampleApp.Sources.democlient
{
    class FleetInfo
    {
        private const string BASE_URI = "https://sandboxapi.deere.com/aemp/";
        private readonly OAuthSignedRestClient _apiClient;

        public FleetInfo(OAuthSignedRestClient apiClient)
        {
            _apiClient = apiClient;
        }

        public void RetrieveFleetDetails()
        {
            RestRequest request = new RestRequest()
            {
                //Start with Fleet/1. Base_URI + "FLEET" is just a redirect to page 1, so starting with page 1 makes everything easier. 
                Url = BASE_URI + "Fleet/1", 
                Method = HttpMethod.Get,
                Accept = "application/xml"
            };

            MakeRecursiveApiCallTillLastPageOfApiResponse(request);
        }

        public void MakeRecursiveApiCallTillLastPageOfApiResponse(RestRequest request)
        {
            var webResponse = _apiClient.Execute(request);

            if (webResponse.StatusCode == HttpStatusCode.OK)
            {
                Fleet fleet = webResponse.GetXMLResponseAsObject<Fleet>(typeof(Fleet));
                PrintFirstEquipmentDetailsFromEachPage(fleet);
                List<Links> links = fleet.Links;
                for (int i = 0; i < links.Count; i++)
                {
                    if (links[i].Rel.Equals("next"))
                    {
                        request.Url = links[i].Href;
                        MakeRecursiveApiCallTillLastPageOfApiResponse(request);
                    }
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"Web Response was not a 200. Actual response is: {webResponse.StatusCode}");
            }
        }

        public void PrintFirstEquipmentDetailsFromEachPage(Fleet fleet)
        {
            List<Equipment> equipmentList = fleet.Equipment;
            foreach (Equipment equipment in equipmentList)
            {
                EquipmentHeader equipmentHeader = equipment.EquipmentHeader;

                if (!equipmentHeader.Model.ToUpper().Equals("UNKNOWN"))
                {
                    System.Diagnostics.Debug.WriteLine("OEM Name = " + equipmentHeader.OEMName);
                    System.Diagnostics.Debug.WriteLine("Model = " + equipmentHeader.Model);
                    System.Diagnostics.Debug.WriteLine("Pin = " + equipmentHeader.PIN);
                    if (equipment.Location != null)
                    {
                        System.Diagnostics.Debug.WriteLine("Location Time= " + equipment.Location.Datetime + " " + "Latitude = " + equipment.Location.Latitude + " " + "Longitude = " + equipment.Location.Longitude);
                    }
                    if (equipment.CumulativeIdleHours != null)
                    {
                        System.Diagnostics.Debug.WriteLine("CumulativeIdleHours Time = " + equipment.CumulativeIdleHours.Datetime + " " + "Hour = " + equipment.CumulativeIdleHours.Hour);
                    }
                    if (equipment.CumulativeLoadCount != null)
                    {
                        System.Diagnostics.Debug.WriteLine("CumulativeLoadCount Time = " + equipment.CumulativeLoadCount.Datetime + " " + "Count = " + equipment.CumulativeLoadCount.Count);
                    }
                    if (equipment.CumulativeOperatingHours != null)
                    {
                        System.Diagnostics.Debug.WriteLine("CumulativeOperatingHours Time = " + equipment.CumulativeOperatingHours.Datetime + " " + "Hour = " + equipment.CumulativeOperatingHours.Hour);
                    }
                    if (equipment.CumulativePayloadTotals != null)
                    {
                        System.Diagnostics.Debug.WriteLine("CumulativePayloadTotals Time = " + equipment.CumulativePayloadTotals.Datetime + " " + "PayloadUnits = " + equipment.CumulativePayloadTotals.PayloadUnits + " " + "Payload = " + equipment.CumulativePayloadTotals.Payload);
                    }
                    if (equipment.DEFRemaining != null)
                    {
                        System.Diagnostics.Debug.WriteLine("DEFRemaining Time = " + equipment.DEFRemaining.Datetime + " " + "Percent = " + equipment.DEFRemaining.Percent);
                    }

                    System.Diagnostics.Debug.WriteLine("");
                    break; // print only the first equipment details from each page
                }
            }
        }
    }

}


