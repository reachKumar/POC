using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Data;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Configuration;
using System.Threading;

namespace BAL
{
    public class MailNotification
    {
        private string _strEmailBody = string.Empty;

        #region Properties
        /// <summary>
        /// The e-mail address of sender
        /// </summary>
        public string MailFromAddress { get; set; }

        /// <summary>
        /// The Display Name which appears in from mail
        /// </summary>
        public string FromDisplayName { get; set; }

        /// <summary>
        /// The e-mail address of receiver. If multiple receiver use semi-comma separated list of e-mail addresses.
        /// </summary>
        public string MailTo { get; set; }

        /// <summary>
        /// The e-mail address of carbon-copy(CC) receipients. If multiple CCs use semi-comma separated list of e-mail addresses.
        /// </summary>
        public string MailCC { get; set; }

        /// <summary>
        /// The e-mail address of backup carbon-copy(BCC) receipients. If multiple BCCs use semi-comma separated list of e-mail addresses.
        /// </summary>
        public string MailBCC { get; set; }

        /// <summary>
        /// The Subject of e-mail
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// The physical path of XSL file used for processing BodyDataset to generate body of an e-mail. Specify MailXSLFile only if BodyDataSet provided.
        /// </summary>
        public string MailXSLFile { get; set; }

        /// <summary>
        /// If body is in HTML format set to true otherwise false. Default to true.
        /// </summary>
        public bool IsBodyHTML { get; set; }

        /// <summary>
        /// Starts new thread to send an email so low pressure on main thread .Default is false.
        /// </summary>
        public bool StartNewThreadToSendMail { get; set; }

        /// <summary>
        /// Specify BodyDataSet only if MailXSLFile is provided
        /// </summary>
        public DataSet BodyDataSet { get; set; }

        /// <summary>
        /// Provide the XML for Mail Body Data
        /// </summary>
        public string DataXML { get; set; }

        /// <summary>
        /// Provide attachments to be sent
        /// </summary>
        public List<string> FilePaths { get; set; }

        /// <summary>
        /// Attachment files
        /// </summary>
        public List<Attachment> Attachments { get; set; }

        #endregion

        #region overloaded SendMail methods

        /// <summary>
        /// Send mail with specified file
        /// </summary>        
        /// <param name="strEmailBodyFilePath">The physical file path where email static body</param>
        public void SendMail(string strEmailBodyFilePath)
        {
            StreamReader reader = new StreamReader(strEmailBodyFilePath);
            _strEmailBody = string.Empty;
            try
            {
                _strEmailBody = reader.ReadToEnd();
            }
            finally
            {
                reader.Close();
            }
            SendMailFromServer();

        }

        /// <summary>
        /// Send mail with specified body
        /// </summary>        
        /// <param name="strMailBody"></param>
        public void SendMail(StringBuilder strMailBody)
        {
            _strEmailBody = strMailBody.ToString();
            SendMailFromServer();
        }

        /// <summary>
        /// Send email message by transforming BodyDataSet and MailXSLFile file to generate body of email message. 
        /// </summary>
        public void SendMail()
        {
            try
            {


                //if (ApplicationServices.HasDataSetRows(_dsBody) && (_strMailXSLFile != string.Empty || _strMailXSLFile != null))
                //{
                //    XmlDocument docXML = new XmlDocument();
                //    docXML.LoadXml(_dsBody.GetXml());
                //    XslCompiledTransform docXSL = new XslCompiledTransform();
                //    docXSL.Load(_strMailXSLFile);
                //}
                //XslTransform oTrasform = new XslTransform();
                //System.Xml.XmlDocument myDoc = new XmlDocument();
                //XsltArgumentList args = new XsltArgumentList();
                //myDoc.LoadXml(_dsBody.GetXml());
                //System.IO.MemoryStream txt = new MemoryStream();
                //System.Xml.XmlTextWriter output = new XmlTextWriter(txt, System.Text.Encoding.UTF8);
                //oTrasform.Load(_strMailXSLFile);
                //oTrasform.Transform(myDoc, args, output, null);
                //output.Flush();
                //txt.Position = 0;
                //StreamReader sr = new StreamReader(txt);
                //_strEmailBody = sr.ReadToEnd();
                if (!string.IsNullOrEmpty(this.DataXML) && (!string.IsNullOrEmpty(this.MailXSLFile)))
                {
                    XmlDocument docXML = new XmlDocument();
                    docXML.LoadXml(this.DataXML);
                    XslCompiledTransform docXSL = new XslCompiledTransform();
                    docXSL.Load(this.MailXSLFile);
                }
                //else if (Utility.HasDataSetRows(this.BodyDataSet) && (!string.IsNullOrEmpty(this.MailXSLFile)))
                //{
                //    XmlDocument docXML = new XmlDocument();
                //    docXML.LoadXml(this.BodyDataSet.GetXml());
                //    XslCompiledTransform docXSL = new XslCompiledTransform();
                //    docXSL.Load(this.MailXSLFile);
                //}
                System.Xml.Xsl.XslCompiledTransform oTrasform = new XslCompiledTransform();
                System.Xml.XmlDocument myDoc = new XmlDocument();
                XsltArgumentList args = new XsltArgumentList();
                if (!string.IsNullOrEmpty(this.DataXML))
                    myDoc.LoadXml(this.DataXML);
                else
                    myDoc.LoadXml(this.BodyDataSet.GetXml());
                System.IO.MemoryStream txt = new MemoryStream();
                System.Xml.XmlTextWriter output = new XmlTextWriter(txt, System.Text.Encoding.UTF8);
                oTrasform.Load(this.MailXSLFile);
                oTrasform.Transform(myDoc, args, output);
                //TODO:Transform logic. Find use of XslCompiledTransform
                output.Flush();
                txt.Position = 0;
                StreamReader sr = new StreamReader(txt);
                _strEmailBody = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                //TODO://Use lgging
            }
            SendMailFromServer();
        }

        /// <summary>
        /// Send mail with dataset
        /// </summary>
        /// <param name="bodyDataset"></param>
        /// <param name="xslPhysicalFilePath"></param>
        /// <param name="defaultFrom"></param>
        public static void SendMail(DataSet bodyDataset, string xslPhysicalFilePath, bool defaultFrom)
        {
            //if (BAL.Utility.HasDataSetRows(bodyDataset))
            //{
                BAL.MailNotification objMail = new BAL.MailNotification();
                objMail.MailXSLFile = xslPhysicalFilePath;
                if (!defaultFrom)
                    objMail.MailFromAddress = Convert.ToString(bodyDataset.Tables[0].Rows[0]["EmailFrom"]);
                objMail.MailTo = Convert.ToString(bodyDataset.Tables[0].Rows[0]["EmailTo"]);
                objMail.MailCC = Convert.ToString(bodyDataset.Tables[0].Rows[0]["EmailCC"]);
                objMail.MailBCC = Convert.ToString(bodyDataset.Tables[0].Rows[0]["EmailBCC"]);
                objMail.Subject = Convert.ToString(bodyDataset.Tables[0].Rows[0]["Subject"]);
                objMail.IsBodyHTML = true;
                objMail.BodyDataSet = bodyDataset;
                objMail.StartNewThreadToSendMail = true;
                objMail.SendMail();
            //}
        }

        /// <summary>
        /// Send mail with attachments if any.
        /// </summary>
        /// <param name="bodyDataset"></param>
        /// <param name="xslPhysicalFilePath"></param>
        /// <param name="defaultFrom"></param>
        /// <param name="attachments">Pass null if attachment is not required</param>
        public static void SendMail(DataSet bodyDataset, string xslPhysicalFilePath, bool defaultFrom, List<Attachment> attachments)
        {
            //if (BAL.Utility.HasDataSetRows(bodyDataset))
            //{
                BAL.MailNotification objMail = new BAL.MailNotification();
                objMail.MailXSLFile = xslPhysicalFilePath;
                if (!defaultFrom)
                    objMail.MailFromAddress = Convert.ToString(bodyDataset.Tables[0].Rows[0]["EmailFrom"]);
                objMail.MailTo = Convert.ToString(bodyDataset.Tables[0].Rows[0]["EmailTo"]);
                objMail.MailCC = Convert.ToString(bodyDataset.Tables[0].Rows[0]["EmailCC"]);
                objMail.MailBCC = Convert.ToString(bodyDataset.Tables[0].Rows[0]["EmailBCC"]);
                objMail.Subject = Convert.ToString(bodyDataset.Tables[0].Rows[0]["Subject"]);
                objMail.IsBodyHTML = true;
                objMail.BodyDataSet = bodyDataset;
                objMail.StartNewThreadToSendMail = true;
                objMail.Attachments = attachments;
                objMail.SendMail();
            //}
        }

        #endregion

        #region Send Actual Mail
        /// <summary>
        /// Send actual mail from SMTP Server
        /// </summary>
        private void SendMailFromServer()
        {
            try
            {
                //Initialize some variables
                MailMessage emailMsg = null;// new MailMessage();
                //bool blnIsDevEnviornment = false;
                MailAddress fromMailAddress = null;
                MailAddress toMailAddress = null;
                string[] arremailIds = null;
                string strEmailIds = string.Empty;

                if (!string.IsNullOrEmpty(this.FromDisplayName))
                {
                    this.FromDisplayName = this.FromDisplayName.Trim();
                }
                else
                {
                    this.FromDisplayName = "";
                }
                if (!string.IsNullOrEmpty(this.MailFromAddress))
                    this.MailFromAddress = this.MailFromAddress.Trim();
                this.MailTo = this.MailTo.Trim();
                if (!string.IsNullOrEmpty(this.MailCC))
                    this.MailCC = this.MailCC.Trim();
                if (!string.IsNullOrEmpty(this.Subject))
                    this.Subject = this.Subject.Trim();
                else
                    this.Subject = "No subject";

                //if (ConfigurationManager.AppSettings["ENVIORNMENT"].ToString().ToUpper().Trim() == "DEV")
                //    blnIsDevEnviornment = true;

                //Set From Mail address

                if (!string.IsNullOrEmpty(this.MailFromAddress))
                {
                    //fromMailAddress = new MailAddress("\"" + this.FromDisplayName + "\" <" + ConfigurationManager.AppSettings["FromMailId"].ToString().Trim() + ">");
                    if (this.FromDisplayName != null && this.FromDisplayName != "")
                    {
                        fromMailAddress = new MailAddress("\"" + this.FromDisplayName + "\" <" + this.MailFromAddress.Trim() + ">");
                    }
                    else
                    {
                        fromMailAddress = new MailAddress(this.MailFromAddress.Trim());
                    }
                }
                else
                    fromMailAddress = new MailAddress(ConfigurationManager.AppSettings["FromMailId"].ToString().Trim());


                //if (blnIsDevEnviornment)
                //{// If enviornment is development do not send mail to actual recipients

                    //Set To Mail addresses
                //    strEmailIds = ConfigurationManager.AppSettings["ToMailIdForDev"].ToString().Trim();
                //    string toEmailId = string.Empty;
                //    if (strEmailIds.Contains(";"))
                //        toEmailId = strEmailIds.Substring(0, strEmailIds.IndexOf(";")).Trim();//take first Recipient only. Other add later
                //    else
                //        toEmailId = strEmailIds;
                //    toMailAddress = new MailAddress(toEmailId);
                //    emailMsg = new MailMessage(fromMailAddress, toMailAddress);
                //    // set body prefix text to show actual receipeints and CCs
                //    if (this.IsBodyHTML)
                //        _strEmailBody = @"Actual receipeints=" + this.MailTo + @"<br /> Actual CCs=" + this.MailCC + @"<br /> Actual BCCs=" + this.MailBCC + @" <br />" + _strEmailBody;
                //    else
                //        _strEmailBody = "Actual receipeints=" + this.MailTo + " Actual CCs=" + this.MailCC + " Actual BCCs=" + this.MailBCC + _strEmailBody;
                //}
                //else
                //{
                    //Set To Mail addresses
                    strEmailIds = this.MailTo;
                    string toEmailId = string.Empty;
                    if (strEmailIds.Contains(";"))
                        toEmailId = strEmailIds.Substring(0, strEmailIds.IndexOf(";")).Trim();//take first Recipient only. Others add later
                    else
                        toEmailId = strEmailIds;
                    if (!String.IsNullOrEmpty(toEmailId))
                    {
                        toMailAddress = new MailAddress(toEmailId);
                        emailMsg = new MailMessage(fromMailAddress, toMailAddress);
                    }
                    //Add CC
                    int intCounter;
                    if (!String.IsNullOrEmpty(this.MailCC))
                    {
                        arremailIds = this.MailCC.Split(';');
                        for (intCounter = 0; intCounter < arremailIds.Length; intCounter++)
                        {
                            if (arremailIds[intCounter].Length > 0)
                                emailMsg.CC.Add(arremailIds[intCounter].Trim());
                        }
                    }
                    if (!String.IsNullOrEmpty(this.MailBCC))
                    {
                        arremailIds = this.MailBCC.Split(';');
                        for (intCounter = 0; intCounter < arremailIds.Length; intCounter++)
                        {
                            if (arremailIds[intCounter].Length > 0)
                                emailMsg.Bcc.Add(arremailIds[intCounter].Trim());
                        }
                    }
                //}

                //****************** Add additional email ids in To ***********
                arremailIds = strEmailIds.Split(';');
                for (int intCounter1 = 0; intCounter1 < arremailIds.Length; intCounter1++)
                {
                    if (intCounter1 == 0)
                        continue;//skip first email id as we have already added it
                    else
                    {
                        if (arremailIds[intCounter1].Length > 0)
                            emailMsg.To.Add(arremailIds[intCounter1].Trim());
                    }
                }

                // Attachments
                if (this.FilePaths != null)
                {
                    if (this.FilePaths.Count > 0)
                    {
                        foreach (string strFilePath in this.FilePaths)
                        {
                            emailMsg.Attachments.Add(new Attachment(strFilePath));
                        }
                    }
                }
                if (this.Attachments != null)
                {
                    if (this.Attachments.Count > 0)
                    {
                        foreach (Attachment attachmentItem in this.Attachments)
                        {
                            emailMsg.Attachments.Add(attachmentItem);
                        }
                    }
                }
                //*******************************************
                //Set subject of mail message
                if (!string.IsNullOrEmpty(this.Subject))
                    emailMsg.Subject = this.Subject;
                else
                    emailMsg.Subject = "No subject specified";
                emailMsg.IsBodyHtml = this.IsBodyHTML;
                emailMsg.Body = _strEmailBody;
                emailMsg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                string smtpServer = ConfigurationManager.AppSettings["SMTPServer"].ToString();
                SmtpClient server = new SmtpClient(smtpServer);
                //Create a separate thread to send email. So low pressure on main thread
                if (this.StartNewThreadToSendMail)
                {
                    Thread threadMail = new Thread(delegate() { server.Send(emailMsg); });
                    threadMail.IsBackground = true;
                    threadMail.Priority = ThreadPriority.Lowest;
                    threadMail.Start();
                }
                else
                    server.Send(emailMsg);

            }
            catch (Exception ex)
            {
                string strFilePath = "MailNotification.cs";
                string strActionBy = "WebServer";
                if (HttpContext.Current != null)
                    if (HttpContext.Current.Request != null)
                    {
                        if (HttpContext.Current.Request.FilePath != null)
                            strFilePath = HttpContext.Current.Request.FilePath;
                        if (HttpContext.Current.Session["NTUserId"] != null)
                            strActionBy = HttpContext.Current.Session["NTUserId"].ToString();
                    }
                //Utility.HandleException(ex, ExceptionPolicyName.BUSINESS_LOGIC_POLICY);
            }
        }
        #endregion
    }
}