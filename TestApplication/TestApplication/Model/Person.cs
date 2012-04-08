using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace TestApplication.Model {
    public class Person : IDataErrorInfo {
     char[] iChars = { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '+', '=', '-', '[', ']', '\\', ',', ';', '.', '/', '{', '}', '|', '\\', '"', ':', '<', '>', '?', '_' };
     string phoneExp = @"^\d{3}\d{3}\d{4}$";
     string MatchEmailPattern =
                 @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|" +
               @"0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z]" +
               @"[a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";



     private  int _id;
     public virtual int ID {
         get { return _id; }
         set { _id = value; }
     }

     private string _firstName;
     public virtual string Firstname {
         get { return _firstName; }
         set { _firstName=value;}
     }

     private   string _contactNumber = "";
     public virtual string ContactNumber {
         get { return _contactNumber; }
         set { _contactNumber = value; }
     }

     private  string _emailid = "";
     public virtual string EmailId {
         get { return _emailid; }
         set { _emailid = value; }
     }


     public virtual string Error {
         get { return null; }
     }

     public virtual string this[string columnName] {
         get {
             string error = null;
             switch (columnName) {
                 case "ID": if(ID<0){
                             error = "Please enter an ID";                 
                             }
                     break;
                 case "FirstName":
                     if (string.IsNullOrEmpty(_firstName)) {
                         error = "First Name required";
                     }
                     else if (_firstName.Length < 0 || _firstName.Length > 20) {
                         error = "The CoreLabel should be between 1- 20 characters";
                     }
                     else {
                         foreach (char c in _firstName) {
                             foreach (char v in iChars) {
                                 if (c == v || c < 65 || c > 122) {
                                     error = "Special characters and Numbers are not allowed.\n Please remove them and try again.";
                                 }
                             }

                         }
                     }
                     break;

                 case "ContactNumber":
                 
                     if ((Regex.Match(_contactNumber, phoneExp).Success) == false) {
                         Console.WriteLine("The contact number is {0}", Regex.Match(_contactNumber, phoneExp).Success);
                         error = "Please enter ContactNumber in the format 1234567890";
                     }                    
                
                     break;

                 case "EmailId":
                    if((Regex.Match(_emailid, MatchEmailPattern).Success)==false) {
                         error = "Enter a vlaid Email ID ";
                    
                     }
                      break;
             }
             return (error);

             
         }
     }

    }
}
