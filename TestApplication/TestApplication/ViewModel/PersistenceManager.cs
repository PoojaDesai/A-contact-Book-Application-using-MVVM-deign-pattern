using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;
using System.Reflection;
//using NHibernate.Expression;
using System.Data.SqlClient;
using System.Collections.Specialized;
using NHibernate;
using NHibernate.Cfg;
using System.Configuration;
using System.Data;
using TestApplication.Model;


namespace TestApplication.Model {

    public enum SessionAction { Begin, Continue, End, BeginAndEnd }


    public class PersistenceManager  {

        private ISessionFactory m_SessionFactory = null;
        private ISession m_Session = null;
        private string con = ConfigurationManager.ConnectionStrings["TestApplicationConnection"].ConnectionString;
  
        public PersistenceManager() {
            this.ConfigureNHibernate();        
        }

        /// <summary>
        /// Configures NHibernate and creates a member-level session factory.
        /// </summary>
        private void ConfigureNHibernate() {
            // Initialize
            NHibernate.Cfg.Configuration cfg = new NHibernate.Cfg.Configuration();
            cfg.Configure();

            /* Note: The AddAssembly() method requires that mappings be 
             * contained in hbm.xml files whose BuildAction properties 
             * are set to ‘Embedded Resource’. */

            // Add class mappings to configuration object
            Assembly thisAssembly = typeof(Person).Assembly;
            cfg.AddAssembly(thisAssembly);

            // Create session factory from configuration object
            m_SessionFactory = cfg.BuildSessionFactory();
        }


       

        /// <summary>
        /// Saves an object and its persistent children.
        /// </summary>
             public void Save<T>(T item) {    
              m_Session = m_SessionFactory.OpenSession() ;
              System.Diagnostics.Debug.WriteLine((m_Session.GetType()));
              m_Session.BeginTransaction();        
              m_Session.SaveOrUpdate(item);
              m_Session.Transaction.Commit();
              m_Session.Flush();
              m_Session.Close();           
             }


             

         //<summary>
         //Deletes an object of a specified type.
         //</summary>
         //<param name="itemsToDelete">The items to delete.</param>
         //<typeparam name="T">The type of objects to delete.</typeparam>
            public void Delete<T>(T item) {
                using (ISession session = m_SessionFactory.OpenSession()) {
                    using (session.BeginTransaction()) {
                        session.Delete(item);
                        session.Transaction.Commit();
                    }
                }
            }

            // public virtual void LoadBusinessObjects() {
            /// <summary>
            /// Retrieves all objects of a given type.
            /// </summary>
            /// <typeparam name="T">The type of the objects to be retrieved.</typeparam>
            /// <returns>A list of all objects of the specified type.</returns>
            public IList<T> RetrieveAll<T>(SessionAction sessionAction) {
                // Open a new session if specified
                if ((sessionAction == SessionAction.Begin) || (sessionAction == SessionAction.BeginAndEnd)) {
                    m_Session = m_SessionFactory.OpenSession();
                }

                // Retrieve all objects of the type passed in
                ICriteria targetObjects = m_Session.CreateCriteria(typeof(T));
                IList<T> itemList = targetObjects.List<T>();

                // Close the session if specified
                if ((sessionAction == SessionAction.End) || (sessionAction == SessionAction.BeginAndEnd)) {
                    m_Session.Close();
                    m_Session.Dispose();
                }
                return itemList;

            }



           
           

    }
}
