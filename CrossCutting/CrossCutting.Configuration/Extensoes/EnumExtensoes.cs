using System.ComponentModel;
using System.Reflection;

namespace CrossCutting.Configuration.Extensoes
{
    public static class EnumExtensoes
    {
        public static string ObterDescricao(this Enum valor)
        {
            FieldInfo campo = valor.GetType().GetField(valor.ToString());

            if (campo != null &&
                Attribute.GetCustomAttribute(campo, typeof(DescriptionAttribute)) is DescriptionAttribute atributo)
            {
                return atributo.Description;
            }

            return valor.ToString();
        }
    }
}
