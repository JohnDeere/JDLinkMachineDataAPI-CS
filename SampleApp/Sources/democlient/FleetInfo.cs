using Hammock.Authentication.OAuth;
using System.IO;
using System.Collections.Generic;

namespace SampleApp.Sources.democlient
{
    class FleetInfo
    {
        string BASE_URI = "https://sandboxapi.deere.com/aemp/";
        public void retrieveFleetDetails()
        {
            OAuthCredentials credentials = OAuthWorkFlow.createOAuthCredentials(OAuthType.ProtectedResource, ApiCredentials.TOKEN.token,
                ApiCredentials.TOKEN.secret, null, null);


            Hammock.RestClient client = new Hammock.RestClient()
            {
                Authority = "",
                Credentials = credentials
            };

            Hammock.RestRequest request = new Hammock.RestRequest()
            {
                Path = BASE_URI + "Fleet"
            };

            request.AddHeader("Accept", "application/xml");

            Hammock.RestResponse response = client.Request(request);
            request.Path = BASE_URI + response.Headers.Get("Location");

            System.Diagnostics.Debug.WriteLine("");

            makeRecursiveApiCallTillLastPageOfApiResponse(request, credentials, client);
        }

        public void makeRecursiveApiCallTillLastPageOfApiResponse(Hammock.RestRequest request, OAuthCredentials credentials, Hammock.RestClient client)
        {
            Hammock.RestResponse response = client.Request(request);
            Fleet fleet = Deserialise<Fleet>(response.ContentStream);
            printFirstEquipmentDetailsFromEachPage(fleet);
            List<Links> links = fleet.Links;
            for (int i = 0; i < links.Count; i++)
            {
                if (links[i].Rel.Equals("next"))
                {
                    request.Path = links[i].Href;
                    makeRecursiveApiCallTillLastPageOfApiResponse(request, credentials, client);
                }
            }
        }

         public static T Deserialise<T>(Stream stream)
        {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                T result = (T)serializer.Deserialize(stream);
                return result;
        }
        public void printFirstEquipmentDetailsFromEachPage(Fleet fleet)
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


