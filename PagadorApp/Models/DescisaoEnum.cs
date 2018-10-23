using System.ComponentModel;
using System.Reflection;
using PagadorApp.Models.Enum;



namespace PagadorApp.Models
{
    public class DescisaoEnum
    {
        public static string GetDescription(string tipo, int valor)

        {
            string descricao = "";

            if (tipo == "StatusTransacaoEnum")

            {
                System.Enum.GetName(typeof(StatusTransacaoEnum), valor);
                StatusTransacaoEnum en = (StatusTransacaoEnum)valor;
                FieldInfo fi = en.GetType().GetField(en.ToString());

            }

            return descricao;


        }

    }

}
