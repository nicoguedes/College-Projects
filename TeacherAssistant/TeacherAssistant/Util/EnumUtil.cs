using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Web.Mvc;

namespace TeacherAssistant.Util
{
    public class EnumUtil
    {
        public static SelectList EnumTypeToSelectList(Type enumType)
        {
            List<SelectListItem> itens = new List<SelectListItem>();

            var enumItens = Enum.GetValues(enumType);
            foreach (Enum en in enumItens)
            {
                DescriptionAttribute enumDesc = GetEnumAttribute<DescriptionAttribute>(en);
                string description = enumDesc != null ? enumDesc.Description : en.ToString();

                Type underlyingType = Enum.GetUnderlyingType(enumType);
                string value = Convert.ToString(Convert.ChangeType(en, underlyingType));

                itens.Add(new SelectListItem() { Value = value, Text = description });
            }

            return new SelectList(itens, "Value", "Text");
        }

        public static T GetEnumAttribute<T>(Enum enumValue)
        {
            return enumValue.GetType().GetMember(enumValue.ToString())[0].GetCustomAttributes(typeof(T), false).Cast<T>().SingleOrDefault();
        }
    }
}