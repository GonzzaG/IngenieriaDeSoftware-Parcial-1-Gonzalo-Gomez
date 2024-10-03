using BEL;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class UsuarioDAL
    {
        private static int mId;

        #region Métodos privados

        private static void ValorizarEntidad(Usuario pUsuario, DataRow pDr)
        {
            pUsuario.Id = (int)pDr["Usuario_Id"];
            pUsuario.EstablecerNombre(pDr["Usuario_Nombre"].ToString());
            pUsuario.EstablecerContrasena(pDr["Usuario_Contrasena"].ToString());
        }

        #endregion Métodos privados

        public static int ProximoId()
        {
            if (mId == 0)
                mId = new DAO().ObtenerUltimoId("Usuario", "Usuario_Id");

            mId += 1;
            return mId;
        }

        public static int Guardar(Usuario pUsuario)
        {
            if (pUsuario.Id == 0)
            {
                pUsuario.Id = ProximoId();
                string mCommandText = $"INSERT INTO Usuario (Usuario_Id, Usuario_Nombre, Usuario_Contrasena) " +
                    $"VALUES ({pUsuario.Id}, '{pUsuario.Nombre}', '{pUsuario.Contrasena}')";
                return new DAO().ExecuteNonQuery(mCommandText);
            }
            else
            {
                string mCommandText = $"UPDATE Usuario SET Usuario_Nombre = '{pUsuario.Nombre}, Usuario_Contrasena = '{pUsuario.Contrasena}' " +
                    $"WHERE Usuario_Id = {pUsuario.Id}";
                return new DAO().ExecuteNonQuery(mCommandText);
            }
        }

        public static void Eliminar(Usuario pUsuario)
        {
            string mCommandText = $"DELETE FROM Usuario WHERE Usuario_Id = {pUsuario.Id}";
            new DAO().ExecuteNonQuery(mCommandText);
        }

        public static List<Usuario> Listar()
        {
            string mCommandText = "SELECT * FROM Usuario";
            DataSet mDs = new DAO().ExecuteDataSet(mCommandText);
            List<Usuario> ListaUsuarios = new List<Usuario>();
            foreach (DataRow mDr in mDs.Tables[0].Rows)
            {
                Usuario mUsuario = new Usuario();
                ValorizarEntidad(mUsuario, mDr);
                ListaUsuarios.Add(mUsuario);
            }

            return ListaUsuarios;
        }

        public static Usuario Obtener(int pId)
        {
            string mCommandText = $"SELECT * FROM Usuario WHERE Usuario_Id = {pId}";
            DataSet mDs = new DAO().ExecuteDataSet(mCommandText);

            if (mDs.Tables.Count > 0 && mDs.Tables[0].Rows.Count > 0)
            {
                Usuario mUsuario = new Usuario();
                ValorizarEntidad(mUsuario, mDs.Tables[0].Rows[0]);
                return mUsuario;
            }
            else
            {
                return null;
            }
        }

        public static Usuario ObtenerPorNombreYContrasena(string pNombre, string pContrasena)
        {
            string mCommandText = $"SELECT * FROM Usuario WHERE Usuario_Nombre = '{pNombre}' AND Usuario_Contrasena = '{pContrasena}'";
            DataSet mDs = new DAO().ExecuteDataSet(mCommandText);

            if (mDs.Tables.Count > 0 && mDs.Tables[0].Rows.Count > 0)
            {
                Usuario mUsuario = new Usuario();
                ValorizarEntidad(mUsuario, mDs.Tables[0].Rows[0]);
                return mUsuario;
            }
            else
            {
                return null;
            }
        }
    }
}