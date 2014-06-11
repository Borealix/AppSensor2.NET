using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
/* 
 * This is another library's utility that doesn't exist for C#
 * Source: http://antipatterns.blogspot.mx/2012/03/c-reflection-to-automatically-copy-and.html
 * 
 * Migrator's note.
 */
namespace Tools.CopyProperties {

    public class ReflectionUtils {
        /// <summary> 
        /// copy ALL the values of public properties, that have the same name, from source to dest. 
        /// ignores properties that have different names. 
        ///  
        /// note - this would also perform shallow copy of a collection property, if is has the same name! 
        /// </summary> 
        /// <param name="source"></param> 
        /// <param name="dest"></param> 
        /// returns: list of properties that were copied 
        public static List<string> CopyProperties(object source, object dest, string[] propertiesToExclude = null) {
            
            List<string> propsCopied = new List<string>();
            PropertyInfo[] properties = getProperties(source);
            
            if(propertiesToExclude == null)
            
                propertiesToExclude = new string[] { };
            
            foreach(var prop in properties) {
                
                if(!propertiesToExclude.Contains(prop.Name) && hasProperty(dest, prop)) {
                
                    object val = getPropertyValue(source, prop);

                    //trim strings (especially for Excel source which tends to have lots of whitespace) 

                    if(val != null && val.GetType() == typeof(string)) {

                        string str = (string)val;

                        val = str.Trim();

                    }

                    setProperty(dest, prop, val);

                    propsCopied.Add(prop.Name);

                }

            }

            return propsCopied;

        }

        private static bool hasProperty(object dest, PropertyInfo prop) {

            var props = getProperties(dest);

            //detect case where property has same name, but different value (usually error in client code): 

            var propWithName = props.Where(p => p.Name == prop.Name);

            if(propWithName.Count() == 1) {

                string destPropTypeName = propWithName.First().PropertyType.FullName;

                string sourcePropTypeName = prop.PropertyType.FullName;

                if(destPropTypeName != sourcePropTypeName)

                    throw new ApplicationException("Two properties have same name " + prop.Name + " but different types: dest=" + destPropTypeName + " source:" + sourcePropTypeName);

            }

            return props.Select(p => p.Name).Contains(prop.Name); //need to compare by name (not same objects!) 

        }

        private static object getPropertyValue(object item, PropertyInfo prop) {

            return item.GetType().InvokeMember(prop.Name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty, Type.DefaultBinder, item, new object[] { });

        }

        /// <summary> 
        /// note: if this call fails, it usually means the 2 properties have the same name, but different type. 
        /// </summary> 
        /// <param name="item"></param> 
        /// <param name="prop"></param> 
        /// <param name="val"></param> 
        /// <param name="copyMode"></param> 

        private static void setProperty(object item, PropertyInfo prop, object val) {

            item.GetType().InvokeMember(prop.Name, BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty, Type.DefaultBinder, item, new object[] { val });

        }

        /// <summary> 
        /// get all the public properties of the given object. 
        /// </summary> 
        /// <param name="obj"></param> 
        /// <returns></returns> 

        private static PropertyInfo[] getProperties(object obj) {

            return obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        }

        /// <summary> 
        /// compare all the values of the public properties that have same name. 
        /// ignores properties that have different names. 
        /// </summary> 
        /// <param name="existingLicWEB"></param> 
        /// <param name="prsLic"></param> 
        /// <returns></returns> 

        public static bool IsEqual(object one, object two) {

            PropertyInfo[] props = getProperties(one);

            foreach(var prop in props) {

                if(hasProperty(two, prop)) {

                    object propVal1 = getPropertyValue(one, prop);

                    object propVal2 = getPropertyValue(two, prop);

                    if(propVal1 == null && propVal2 != null)

                        return false;

                    if(propVal1 != null && !propVal1.Equals(propVal2)) {

                        return false;
                    }
                }
            }
            return true;
        }

        public static object GetPropertyValue(object item, string propertyName) {

            var prop = getProperties(item).Where(p => p.Name == propertyName).First();

            return getPropertyValue(item, prop);

        }
    }
}