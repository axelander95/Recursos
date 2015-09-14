using System;
using System.Windows.Forms;

namespace Recursos
{
    class Validator
    {
        public static void OnlyInteger(KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar))
                e.Handled = false;
            else if (Char.IsControl(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        public static bool OnlyEmail(string email)
        {
            char[] array = email.ToCharArray();
            int Count = 0;
            foreach (char caracter in array)
                if (caracter == '@')
                    Count++;
            return (Count == 1);
        }
        public static void NoSpace(KeyPressEventArgs e)
        {
            if (Char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }
        public static void OnlyDecimal(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }


            bool IsDec = false;
            int nroDec = 0;

            for (int i = 0; i < txt.Text.Length; i++)
            {
                if (txt.Text[i] == '.')
                    IsDec = true;

                if (IsDec && nroDec++ >= 4)
                {
                    e.Handled = true;
                    return;
                }
            }

            if (e.KeyChar >= 48 && e.KeyChar <= 57)
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = (IsDec) ? true : false;
            else
                e.Handled = true;
        }
        public static void OnlyLetters(KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || Char.IsWhiteSpace(e.KeyChar) || Char.IsControl(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        public static void OnlyAlphanumeric(KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || Char.IsNumber(e.KeyChar) || Char.IsControl(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }
        public static bool EsCedula(string cedula)
        {
            int isNumeric;
            var total = 0;
            const int length = 10;
            int[] coeficientes = { 2, 1, 2, 1, 2, 1, 2, 1, 2 };
            const int provincias = 24;
            const int tercerDigito = 6;
            if (int.TryParse(cedula, out isNumeric) && cedula.Length == length)
            {
                var provincia = Convert.ToInt32(string.Concat(cedula[0], cedula[1], string.Empty));
                var digitoTres = Convert.ToInt32(cedula[2] + string.Empty);
                if ((provincia > 0 && provincia <= provincias) && digitoTres < tercerDigito)
                {
                    var digitoVerificadorRecibido = Convert.ToInt32(cedula[9] + string.Empty);
                    for (int k = 0; k < coeficientes.Length; k++)
                    {
                        var valor = Convert.ToInt32(coeficientes[k] + string.Empty) * Convert.ToInt32(cedula[k] + string.Empty);
                        total = valor >= 10 ? total + (valor - 9) : total + valor;
                    }
                    var digitoVerificadorObtenido = total >= 10 ? (total % 10) != 0 ? 10 - (total % 10) : (total % 10) : total;
                    return digitoVerificadorObtenido == digitoVerificadorRecibido;
                }
            }
            return false;
        }
        public static bool EsRUCPersonaNatural(string RUC)
        {
            long isNumeric;
            const int length = 13;
            const string establecimiento = "001";
            if (long.TryParse(RUC, out isNumeric) && RUC.Length.Equals(length))
            {
                var numeroProvincia = Convert.ToInt32(string.Concat(RUC[0] + string.Empty, RUC[1] + string.Empty));
                var personaNatural = Convert.ToInt32(RUC[2] + string.Empty);
                if ((numeroProvincia >= 1 && numeroProvincia <= 24) && (personaNatural >= 0 && personaNatural < 6))
                    return RUC.Substring(10, 3) == establecimiento && EsCedula(RUC.Substring(0, 10));
                return false;
            }
            return false;
        }
        public static bool EsRUCPersonaJuridica(string ruc)
        {
            int[] coeficientes = { 4, 3, 2, 7, 6, 5, 4, 3, 2 };
            bool resp_dato = false;
            int prov = int.Parse(ruc.Substring(0, 2));
            int constante = 11;
            if (ruc.Length == 13)
            {
                if (!((prov > 0) && (prov <= 24)))
                {
                    resp_dato = false;
                }

                int[] d = new int[10];
                int suma = 0;

                for (int i = 0; i < d.Length; i++)
                {
                    d[i] = int.Parse(ruc[i] + string.Empty);
                }

                for (int i = 0; i < d.Length - 1; i++)
                {
                    d[i] = d[i] * coeficientes[i];
                    suma += d[i];
                }

                int aux, resp;

                aux = suma % constante;
                resp = constante - aux;

                resp = (aux == 0) ? 0 : resp;

                if (resp == d[9])
                {
                    resp_dato = true;
                }
                else
                {
                    resp_dato = false;
                }
            }
            return resp_dato;
        }
        public static bool EsRUCSociedadPublica(string ruc)
        {
            if (ruc.Length == 13)
            {
                long isNumeric;
                const int length = 13;
                const int modulo = 11;
                const int tercerDigito = 6;
                var total = 0;
                const string establecimiento = "0001";
                int[] coeficientes = { 3, 2, 7, 6, 5, 4, 3, 2 };
                if (long.TryParse(ruc, out isNumeric) && ruc.Length.Equals(length))
                {
                    var numeroProvincia = Convert.ToInt32(string.Concat(ruc[0] + string.Empty, ruc[1] + string.Empty));
                    var sociedadPublica = Convert.ToInt32(ruc[2] + string.Empty);
                    if ((numeroProvincia >= 1 && numeroProvincia <= 24) && sociedadPublica == tercerDigito && ruc.Substring(9, 4) == establecimiento)
                    {
                        var digitoVerificadorRecibido = Convert.ToInt32(ruc[0] + string.Empty);
                        for (var i = 0; i < coeficientes.Length; i++)
                            total = total + (coeficientes[i] * Convert.ToInt32(ruc[i] + string.Empty));
                        var digitoVerificadorObtenido = modulo - (total % modulo);
                        return digitoVerificadorObtenido == digitoVerificadorRecibido;
                    }
                }
            }
            return false;
        }
    }
}
