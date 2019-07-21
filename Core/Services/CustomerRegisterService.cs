using Domain.Models;
using Domain.Repositories;
using Domain.ViewModels;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Core.Services
{
    public class CustomerRegisterService
    {
        private UnitOfWork unitOfWork;


        public CustomerRegisterService(UnitOfWork _unitOfWork)
        {

            unitOfWork = _unitOfWork;
        }

       

        string hash = "f0xle@rn";
        public void LogIn(CustomerRegisterViewModel customerRegisterViewModel)
        {
            var CheckPhonenumber = unitOfWork.CustomerregisterRepository.Get().FirstOrDefault(x => x.CustomerPhone == customerRegisterViewModel.CustomerPhone);
            if (CheckPhonenumber != null)
            {
                var custpassword = unitOfWork.CustomerregisterRepository.Get().Where(m => m.CustomerPhone == customerRegisterViewModel.CustomerPhone).Select(k => k.Password).FirstOrDefault();

                var data = Convert.FromBase64String(custpassword);
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                    using (TripleDESCryptoServiceProvider TripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = TripDes.CreateDecryptor();
                        byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

                        var DecriptPassword = UTF8Encoding.UTF8.GetString(result);

                        var existingStatus = unitOfWork.CustomerregisterRepository.Get().FirstOrDefault(x => x.CustomerPhone == customerRegisterViewModel.CustomerPhone && DecriptPassword == customerRegisterViewModel.Password);
                        var isDuplicateDescription = existingStatus != null;
                        if (isDuplicateDescription)
                        {

                            customerRegisterViewModel.Errormessage = "Log In Successfull";
                        }
                        else
                        {

                            customerRegisterViewModel.Errormessage = "Phone Number And Password Not Match";
                        }

                    }
                }
            }
            else
            {
                customerRegisterViewModel.Errormessage = "Phone Number Are Not Match";
            }
            
        }
        
        public void Register(CustomerRegisterViewModel customerRegisterViewModel)
        {


            var existingStatus = unitOfWork.CustomerregisterRepository.Get().FirstOrDefault(x => x.CustomerPhone == customerRegisterViewModel.CustomerPhone);
            var isDuplicateDescription = existingStatus != null;
            if (isDuplicateDescription)
            {
                customerRegisterViewModel.Errormessage = "Phone Number Are Exists";
            }
            else
            {
                byte[] data = UTF8Encoding.UTF8.GetBytes(customerRegisterViewModel.Password);
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                    using (TripleDESCryptoServiceProvider TripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = TripDes.CreateEncryptor();
                        byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

                        var customerregister = new CustomerRegister

                        {



                            CustomerEmail = customerRegisterViewModel.CustomerEmail,
                            CustomerPhone = customerRegisterViewModel.CustomerPhone,
                            Password = Convert.ToBase64String(result, 0, result.Length)

                        };



                        unitOfWork.CustomerregisterRepository.Insert(customerregister);
                        unitOfWork.Save();
                    }
                }
            }



                //MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                //var utf8 = new UTF8Encoding();
                //byte[] data = md5.ComputeHash(utf8.GetBytes(customerRegisterViewModel.Password));

            //    var customerregister = new CustomerRegister

            //    {



            //        CustomerEmail = customerRegisterViewModel.CustomerEmail,
            //        CustomerPhone = customerRegisterViewModel.CustomerPhone,
            //        Password = Convert.ToBase64String(data)

            //    };



            //    unitOfWork.CustomerregisterRepository.Insert(customerregister);
            //    unitOfWork.Save();
            //}


        }
    }
}
