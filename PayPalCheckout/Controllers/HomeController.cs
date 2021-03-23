
using PayPalCheckoutSdk.Orders;
using PayPalCheckoutSdk.Core;
using PayPalHttp;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web.Mvc;
using System.Threading.Tasks;
using PayPalCheckoutSdk.Payments;
using System.Net;
using System.IO;
using Aspose.Pdf;

namespace PayPalCheckout.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        /*BraintreeGateway gateway = new BraintreeGateway("access_token$sandbox$cc7spqgxwjwhtcpm$88fe416e46f6d9e319eb9029b20ce99c")*/

        public ActionResult Creative()
        {
            return Json(new { name = "Aman" });
            //WebRequest req = WebRequest.Create(@"https://blog.aspose.com/2020/02/28/convert-html-to-pdf-programmatically-in-csharp-net/");
            //using (Stream stream = req.GetResponse().GetResponseStream())
            //{
            //    // Initialize HTML load options
            //    HtmlLoadOptions htmloptions = new HtmlLoadOptions("https://blog.aspose.com/");

            //    // Load stream into Document object
            //    Document pdfDocument = new Document(stream, htmloptions);
            //    // Save output as PDF format
            //    pdfDocument.Save("HTML-to-PDF.pdf");
            //}
            //return View();
        }

        public ActionResult Index()
        {
            //var clientToken = gateway.ClientToken.Generate();
            //ViewBag.clientToken = clientToken;
            ViewBag.ClientId = "Af8jLh10kVlYkx4lunz6GSduOt92LyS_hRCOjzRZaU1SZrr1Eb7xYXBjLn-ue6SqiVlsTCEPPlMmL4bI";
            //   ViewBag.ClientId = "ATX3PupfhuN__lU4HiU-Z6uItv6VwdQpFXHENptZYbY5_eJih3wujtYKYvisP6HKmqBv284neWr4bLz4";
            return View();
        }
        [HttpPost]
        public async Task<String> CreateOrder(bool debug = false)
        {
            try
            {
                var request = new OrdersCreateRequest();
                request.Prefer("return=representation");
                request.RequestBody(BuildRequestBody());
                //3. Call PayPal to set up a transaction


                PayPalEnvironment payPalEnvironment = new SandboxEnvironment("Af8jLh10kVlYkx4lunz6GSduOt92LyS_hRCOjzRZaU1SZrr1Eb7xYXBjLn-ue6SqiVlsTCEPPlMmL4bI", "EKQBNJEbuBx-yIx0vdtFTiptyfnnyjQVYwhPAmiuKe19T3Viko-B8E-wYrfQuvGdVCOJ97zH9-arreb4");
                //    PayPalEnvironment payPalEnvironment = new LiveEnvironment("ATX3PupfhuN__lU4HiU-Z6uItv6VwdQpFXHENptZYbY5_eJih3wujtYKYvisP6HKmqBv284neWr4bLz4", "EE4f6LT30Fkbi2QQFNRn9umiXv-opRgyTaaqpbkVfPAlXtvIqkywag-Sqczu9GsoF1Brx4FGmLOP7xil");

                var httpResponse = await new PayPalHttpClient(payPalEnvironment).Execute(request);



                //var result = httpResponse.Result;

                var result = httpResponse.Result<Order>();
                //Console.WriteLine("Status: {0}", result.Status);
                //Console.WriteLine("Order Id: {0}", result.Id);
                //Console.WriteLine("Intent: {0}", result.Payer);
                //Console.WriteLine("Links:");
                //foreach (LinkDescription link in result.Links)
                //{
                //    Console.WriteLine("\t{0}: {1}\tCall Type: {2}", link.Rel, link.Href, link.Method);
                //}
                //AmountWithBreakdown amount = result.PurchaseUnits[0].AmountWithBreakdown;
                //Console.WriteLine("Total Amount: {0} {1}", amount.CurrencyCode, amount.Value);

                return result.Id;
            }

            catch (Exception e)
            {
                var request = new OrdersCreateRequest();
                //PayPalEnvironment payPalEnvironment = new SandboxEnvironment("Af8jLh10kVlYkx4lunz6GSduOt92LyS_hRCOjzRZaU1SZrr1Eb7xYXBjLn-ue6SqiVlsTCEPPlMmL4bI", "EKQBNJEbuBx-yIx0vdtFTiptyfnnyjQVYwhPAmiuKe19T3Viko-B8E-wYrfQuvGdVCOJ97zH9-arreb4");
                PayPalEnvironment payPalEnvironment = new SandboxEnvironment("AWslfpHdNCbQxriW7vju-2SyILIfD62prHjvERnaG_G7B1WmGAP6Spw04MMwJBixLWd5peQ-3JQ1pasX", "EC-tZkW-enhtWH_Yf3Tmm5R15_9ULsIdfMDU150ivrTyJKUyfNioFaR1A7L0bPXPKN3Z3zawNMIp8wqU");
                //return await new PayPalHttpClient(payPalEnvironment).Execute(request);
                return "";
            }

        }

        private static OrderRequest BuildRequestBody()
        {

            //var order = new OrderRequest()
            //{
            //    CheckoutPaymentIntent = "CAPTURE",
            //    PurchaseUnits = new List<PurchaseUnitRequest>()
            //    {
            //        new PurchaseUnitRequest()
            //        {
            //            AmountWithBreakdown = new AmountWithBreakdown()
            //            {
            //                CurrencyCode = "USD",
            //                Value = "100.00"
            //            }
            //        }
            //    },
            //    ApplicationContext = new ApplicationContext()
            //    {
            //        ReturnUrl = "https://www.example.com",
            //        CancelUrl = "https://www.example.com"
            //    }
            //};

            var orderRequest = new OrderRequest()
            {
                CheckoutPaymentIntent = "CAPTURE",

                ApplicationContext = new ApplicationContext
                {
                    BrandName = "EXAMPLE INC",
                    LandingPage = "BILLING",
                    UserAction = "CONTINUE",
                    ShippingPreference = "SET_PROVIDED_ADDRESS",
                    // ShippingPreference = "GET_FROM_FILE",
                    // ShippingPreference = "NO_SHIPPING",
                    ReturnUrl = "http:localhost:8080",
                    CancelUrl = "http:localhost:8080"
                },


                PurchaseUnits = new List<PurchaseUnitRequest>
  {
    new PurchaseUnitRequest{
        InvoiceId = "HW637346612431927080",
      ReferenceId =  "PUHF",
      Description = "Sporting Goods",
      CustomId = "CUST-HighFashions",
      SoftDescriptor = "HighFashions",
      AmountWithBreakdown = new AmountWithBreakdown
      {
        CurrencyCode = "USD",
        Value = "230.00",
        AmountBreakdown = new AmountBreakdown
        {
          ItemTotal = new PayPalCheckoutSdk.Orders.Money
          {
            CurrencyCode = "USD",
            Value = "180.00"
          },
          Shipping = new PayPalCheckoutSdk.Orders.Money
          {
            CurrencyCode = "USD",
            Value = "30.00"
          },
          Handling = new PayPalCheckoutSdk.Orders.Money
          {
            CurrencyCode = "USD",
            Value = "10.00"
          },
          TaxTotal = new PayPalCheckoutSdk.Orders.Money
          {
            CurrencyCode = "USD",
            Value = "20.00"
          },
          ShippingDiscount = new PayPalCheckoutSdk.Orders.Money
          {
            CurrencyCode = "USD",
            Value = "10.00"
          }
        }
      },
      Items = new List<Item>
      {
        new Item
        {
          Name = "T-shirt",
          Description = "Green XL",
          Sku = "sku01",
          UnitAmount = new PayPalCheckoutSdk.Orders.Money
          {
            CurrencyCode = "USD",
            Value = "90.00"
          },
          Tax = new PayPalCheckoutSdk.Orders.Money
          {
            CurrencyCode = "USD",
            Value = "10.00"
          },
          Quantity = "1",
          Category = "PHYSICAL_GOODS"
        },
        new Item
        {
          Name = "Shoes",
          Description = "Running, Size 10.5",
          Sku = "sku02",
          UnitAmount = new PayPalCheckoutSdk.Orders.Money
          {
            CurrencyCode = "USD",
            Value = "45.00"
          },
          Tax = new PayPalCheckoutSdk.Orders.Money
          {
            CurrencyCode = "USD",
            Value = "5.00"
          },
          Quantity = "2",
          Category = "PHYSICAL_GOODS"
        }
      },

      ShippingDetail = new ShippingDetail
      {
        Name = new Name
        {
          FullName = "Aman Soni",

        },
        AddressPortable = new AddressPortable
        {
         //AddressLine1 = "350 /,vijaykiran,crs,domlurlyt,b",
         //           AddressLine2 = "Floor 1",
         //           AdminArea2 = " San Fransisco",
         //           AdminArea1 = "Maharashtra",
         //           PostalCode = "410206",
         //           CountryCode = "IN"

                    AddressLine1 = "123 Townsend St",
                AddressLine2 = "Floor 6",
                AdminArea2 = "San Francisco",
                AdminArea1 = "CA",
                PostalCode = "94107",
                CountryCode = "US"
        },
      //   //Options = new List<ShippingOption>()
      //   //                                  {
      //   //                                      new ShippingOption()
      //   //                                      {
      //   //                                           ShippingType = "PICKUP",
      //   //                                            Id = "1" ,
      //   //                                             Amount = new PayPalCheckoutSdk.Orders.Money
      //   //                                             {
      //   //                                                 CurrencyCode = "US",
      //   //                                                 Value = "0"
      //   //                                             },
      //   //                                              Label = "PICKUP",
      //   //                                              Selected = false
      //   //                                      }
      //   //}

       }
    }
  }
            };


            //PayPalCheckoutSdk.Orders.Payer Payer = new PayPalCheckoutSdk.Orders.Payer
            //{
            //    Email = "aman.soni@47billion.com",
            //    AddressPortable = new AddressPortable
            //    {
            //        AddressLine1 = "350 /,vijaykiran,crs,domlurlyt,b",
            //        AddressLine2 = "Floor 1",
            //        AdminArea2 = " Mumbai",
            //        AdminArea1 = "Maharashtra",
            //        PostalCode = "410206",
            //        CountryCode = "IN"
            //    }
            //};

            //Name Name = new Name
            //{
            //    GivenName = "Aman",
            //    Surname = "Soni"
            //};

            //Payer.Name = Name;
            PhoneWithType phoneWithType = new PhoneWithType() { PhoneType = "MOBILE" };
            PayPalCheckoutSdk.Orders.Phone phone = new PayPalCheckoutSdk.Orders.Phone();
            phone.NationalNumber = "9098368292";
            phoneWithType.PhoneNumber = phone;
          //  Payer.PhoneWithType = phoneWithType;
            //  orderRequest.Payer = Payer;


            return orderRequest;
        }


        public async Task<HttpResponse> CapturesRefund(bool debug = false)
        {
            try
            {
                string CaptureId = "2FR8054805574234G";
                var request = new CapturesRefundRequest(CaptureId);
                request.Prefer("return=representation");
                PayPalCheckoutSdk.Payments.RefundRequest refundRequest = new PayPalCheckoutSdk.Payments.RefundRequest()
                {
                    Amount = new PayPalCheckoutSdk.Payments.Money
                    {
                        Value = "23.00",
                        CurrencyCode = "USD"
                    }
                };
                request.RequestBody(refundRequest);

                PayPalEnvironment payPalEnvironment = new SandboxEnvironment("Af8jLh10kVlYkx4lunz6GSduOt92LyS_hRCOjzRZaU1SZrr1Eb7xYXBjLn-ue6SqiVlsTCEPPlMmL4bI", "EKQBNJEbuBx-yIx0vdtFTiptyfnnyjQVYwhPAmiuKe19T3Viko-B8E-wYrfQuvGdVCOJ97zH9-arreb4");

                var httpResponse = await new PayPalHttpClient(payPalEnvironment).Execute(request);


                if (true)
                {
                    var result = httpResponse.Result<PayPalCheckoutSdk.Payments.Refund>();
                    //result.SellerPayableBreakdown.PaypalFee.Value
                    Console.WriteLine("Status: {0}", result.Status);
                    Console.WriteLine("Refund Id: {0}", result.Id);
                    Console.WriteLine("Links:");
                }
                return httpResponse;
            }
            catch (Exception e)
            {
                var request = new OrdersCreateRequest();
                //PayPalEnvironment payPalEnvironment = new SandboxEnvironment("Af8jLh10kVlYkx4lunz6GSduOt92LyS_hRCOjzRZaU1SZrr1Eb7xYXBjLn-ue6SqiVlsTCEPPlMmL4bI", "EKQBNJEbuBx-yIx0vdtFTiptyfnnyjQVYwhPAmiuKe19T3Viko-B8E-wYrfQuvGdVCOJ97zH9-arreb4");
                PayPalEnvironment payPalEnvironment = new SandboxEnvironment("AWslfpHdNCbQxriW7vju-2SyILIfD62prHjvERnaG_G7B1WmGAP6Spw04MMwJBixLWd5peQ-3JQ1pasX", "EC-tZkW-enhtWH_Yf3Tmm5R15_9ULsIdfMDU150ivrTyJKUyfNioFaR1A7L0bPXPKN3Z3zawNMIp8wqU");
                //return await new PayPalHttpClient(payPalEnvironment).Execute(request);
                var httpResponse = await new PayPalHttpClient(payPalEnvironment).Execute(request);
                return httpResponse;
            }
        }

        public ActionResult GetDetail(object Details)
        {
            return View();
        }

        public async Task<HttpResponse> GetOrder(bool debug = false)
        {
            //string orderId = "8TH80554NC7396247";
            //string orderId = "3WW64787D42227157";
            string orderId = "95868714B0010890H";

            try
            {
                //RefundsGetRequest
                //CapturesGetRequest

                OrdersGetRequest request = new OrdersGetRequest(orderId);

                PayPalEnvironment payPalEnvironment = new SandboxEnvironment("Af8jLh10kVlYkx4lunz6GSduOt92LyS_hRCOjzRZaU1SZrr1Eb7xYXBjLn-ue6SqiVlsTCEPPlMmL4bI", "EKQBNJEbuBx-yIx0vdtFTiptyfnnyjQVYwhPAmiuKe19T3Viko-B8E-wYrfQuvGdVCOJ97zH9-arreb4");

                var response = await new PayPalHttpClient(payPalEnvironment).Execute(request);
                var result = response.Result<Order>();
                Console.WriteLine("Retrieved Order Status");
                var shippingCharge = result.PurchaseUnits.Select(s => s.AmountWithBreakdown).Sum(a => Convert.ToDecimal(a.AmountBreakdown.Shipping.Value));
                var tax = result.PurchaseUnits.Select(s => s.AmountWithBreakdown).Sum(a => Convert.ToDecimal(a.AmountBreakdown.TaxTotal.Value));
                var total = result.PurchaseUnits.Select(s => s.AmountWithBreakdown).Sum(a => Convert.ToDecimal(a.AmountBreakdown.TaxTotal.Value));
                foreach (PurchaseUnit purchaseUnit in result.PurchaseUnits)
                {
                    
                }
                //result.Payer.A
                Console.WriteLine("Status: {0}", result.Status);
                Console.WriteLine("Order Id: {0}", result.Id);
                //Console.WriteLine("Intent: {0}", result.CheckoutPaymentIntent);
                Console.WriteLine("Links:");
                //foreach (LinkDescription link in result.Links)
                //{
                //    Console.WriteLine("\t{0}: {1}\tCall Type: {2}", link.Rel, link.Href, link.Method);
                //}
                // AmountWithBreakdown amount = result.PurchaseUnits[0].AmountWithBreakdown;
                // Console.WriteLine("Total Amount: {0} {1}", amount.CurrencyCode, amount.Value);
                //   Console.WriteLine("Response JSON: \n {0}", PayPalClient.ObjectToJSONString(result));
                return response;

            }
            catch (Exception e)
            {
                OrdersGetRequest request = new OrdersGetRequest(orderId);

                PayPalEnvironment payPalEnvironment = new SandboxEnvironment("Af8jLh10kVlYkx4lunz6GSduOt92LyS_hRCOjzRZaU1SZrr1Eb7xYXBjLn-ue6SqiVlsTCEPPlMmL4bI", "EKQBNJEbuBx-yIx0vdtFTiptyfnnyjQVYwhPAmiuKe19T3Viko-B8E-wYrfQuvGdVCOJ97zH9-arreb4");

                return await new PayPalHttpClient(payPalEnvironment).Execute(request);


            }
        }

        public async Task<HttpResponse> GetTransaction(string OrderId, bool debug = false)
        {
            //string orderId = "8TH80554NC7396247";
            string orderId = "23139793XR804084D";

            try
            {
                //RefundsGetRequest
                //CapturesGetRequest

                var request = new OrdersCaptureRequest(orderId);
                request.RequestBody(new OrderActionRequest());
                PayPalEnvironment payPalEnvironment = new SandboxEnvironment("Af8jLh10kVlYkx4lunz6GSduOt92LyS_hRCOjzRZaU1SZrr1Eb7xYXBjLn-ue6SqiVlsTCEPPlMmL4bI", "EKQBNJEbuBx-yIx0vdtFTiptyfnnyjQVYwhPAmiuKe19T3Viko-B8E-wYrfQuvGdVCOJ97zH9-arreb4");

                var response = await new PayPalHttpClient(payPalEnvironment).Execute(request);
                var result = response.Result<Order>();

                foreach (PurchaseUnit purchaseUnit in result.PurchaseUnits)
                {
                    foreach (PayPalCheckoutSdk.Orders.Capture capture in purchaseUnit.Payments.Captures)
                    {
                        //captureId = capture.Id;
                    }
                }

                Console.WriteLine("Retrieved Order Status");
                //result.Payer.A
                Console.WriteLine("Status: {0}", result.Status);
                Console.WriteLine("Order Id: {0}", result.Id);
                //Console.WriteLine("Intent: {0}", result.CheckoutPaymentIntent);
                Console.WriteLine("Links:");
                //foreach (LinkDescription link in result.Links)
                //{
                //    Console.WriteLine("\t{0}: {1}\tCall Type: {2}", link.Rel, link.Href, link.Method);
                //}
                // AmountWithBreakdown amount = result.PurchaseUnits[0].AmountWithBreakdown;
                // Console.WriteLine("Total Amount: {0} {1}", amount.CurrencyCode, amount.Value);
                //   Console.WriteLine("Response JSON: \n {0}", PayPalClient.ObjectToJSONString(result));
                return response;

            }
            catch (Exception e)
            {
                OrdersGetRequest request = new OrdersGetRequest(orderId);

                PayPalEnvironment payPalEnvironment = new SandboxEnvironment("Af8jLh10kVlYkx4lunz6GSduOt92LyS_hRCOjzRZaU1SZrr1Eb7xYXBjLn-ue6SqiVlsTCEPPlMmL4bI", "EKQBNJEbuBx-yIx0vdtFTiptyfnnyjQVYwhPAmiuKe19T3Viko-B8E-wYrfQuvGdVCOJ97zH9-arreb4");

                return await new PayPalHttpClient(payPalEnvironment).Execute(request);


            }
        }

        public async Task<HttpResponse> CapturesRequest(bool debug = false)
        {
            //string orderId = "8TH80554NC7396247";
            string orderId = "23139793XR804084D";

            try
            {
                //RefundsGetRequest
                //CapturesGetRequest


                var request = new CapturesGetRequest(orderId);

                PayPalEnvironment payPalEnvironment = new SandboxEnvironment("Af8jLh10kVlYkx4lunz6GSduOt92LyS_hRCOjzRZaU1SZrr1Eb7xYXBjLn-ue6SqiVlsTCEPPlMmL4bI", "EKQBNJEbuBx-yIx0vdtFTiptyfnnyjQVYwhPAmiuKe19T3Viko-B8E-wYrfQuvGdVCOJ97zH9-arreb4");

                var rr = new AuthorizationsCaptureRequest(orderId);

                var response = await new PayPalHttpClient(payPalEnvironment).Execute(rr);
                var result = response.Result<Order>();
                Console.WriteLine("Retrieved Order Status");
                //result.Payer.A
                Console.WriteLine("Status: {0}", result.Status);
                Console.WriteLine("Order Id: {0}", result.Id);
                //Console.WriteLine("Intent: {0}", result.CheckoutPaymentIntent);
                Console.WriteLine("Links:");
                //foreach (LinkDescription link in result.Links)
                //{
                //    Console.WriteLine("\t{0}: {1}\tCall Type: {2}", link.Rel, link.Href, link.Method);
                //}
                // AmountWithBreakdown amount = result.PurchaseUnits[0].AmountWithBreakdown;
                // Console.WriteLine("Total Amount: {0} {1}", amount.CurrencyCode, amount.Value);
                //   Console.WriteLine("Response JSON: \n {0}", PayPalClient.ObjectToJSONString(result));
                return response;

            }
            catch (Exception e)
            {
                OrdersGetRequest request = new OrdersGetRequest(orderId);

                PayPalEnvironment payPalEnvironment = new SandboxEnvironment("Af8jLh10kVlYkx4lunz6GSduOt92LyS_hRCOjzRZaU1SZrr1Eb7xYXBjLn-ue6SqiVlsTCEPPlMmL4bI", "EKQBNJEbuBx-yIx0vdtFTiptyfnnyjQVYwhPAmiuKe19T3Viko-B8E-wYrfQuvGdVCOJ97zH9-arreb4");

                return await new PayPalHttpClient(payPalEnvironment).Execute(request);


            }
        }

    }
}