﻿using AutoMapper;
using ItServiceApp.Models;
using ItServiceApp.Models.Payment;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.Extensions.Configuration;
using MUsefulMethods;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ItServiceApp.Services
{
    public class IyzicoPaymentService : IPaymentService
    {
       
       private readonly IConfiguration _configuration;
       private readonly IyzicoPaymentOptions _options;
        private readonly IMapper _mapper;


        public IyzicoPaymentService (IConfiguration configuration, IMapper mapper)
        {
            _mapper=mapper;
            _configuration = configuration;
            var section = configuration.GetSection(IyzicoPaymentOptions.Key);
            _options = new IyzicoPaymentOptions()
            {
                ApiKey = section["ApiKey"],
                SecretKey=section["SecretKey"],
                BaseUrl=section["BaseUrl"],
                ThreedsCallbackUrl=section["ThreedsCallbackUrl"],


            };
        }
        private string GenerateConversationId()
        {
            return StringHelpers.GenerateUniqueCode();
        }
        public InstallmentModel CheckInstallments(string binNumber,decimal price)
        {
            var conversationId=GenerateConversationId();
            var request = new RetrieveInstallmentInfoRequest
            {
                Locale = Locale.TR.ToString(),
                ConversationId=conversationId,
                BinNumber = binNumber,
                Price = price.ToString(new CultureInfo("en-Us")),
            };
            var result = InstallmentInfo.Retrieve(request, _options);
            if (result.Status == "failure") 
            { 
                throw new Exception(result.ErrorMessage);
            }
            if (result.ConversationId != conversationId)
            {
                throw new Exception("Hatalı istek oluşturuldu");
            }

            var resultModel = _mapper.Map<InstallmentModel>(result.InstallmentDetails[0]);
            Console.WriteLine();
            return resultModel;
        }

        public PaymentResponseModel Pay(PaymentModel model)
        {
            return null;
        }
    }
}
