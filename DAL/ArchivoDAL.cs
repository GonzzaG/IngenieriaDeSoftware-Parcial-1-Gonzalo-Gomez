using BEL;
using System.Collections.Generic;
using System.Data;

namespace DAL
{
    public class ArchivoDAL
    {
        private static int mId;

        #region Métodos privados

        private static void ValorizarEntidad(Archivo pArchivo, DataRow pDr)
        {
            pArchivo.Id = (int)pDr["Archivo_Id"];
            pArchivo.Nombre = pDr["Archivo_Nombre"].ToString();
            pArchivo.DirectorioId = (int)pDr["Directorio_Id"];
            pArchivo.Tamano = float.Parse(pDr["Archivo_Tamano"].ToString());
        }

        #endregion Métodos privados

        public static int ProximoId()
        {
            if (mId == 0)
                mId = new DAO().ObtenerUltimoId("Archivo", "Archivo_Id");

            mId += 1;
            return mId;
        }

        #region Métodos públicos

        public static int Guardar(Archivo pArchivo)
        {
            if (pArchivo.Id == 0) // no existe, Insertar
            {
                pArchivo.Id = ProximoId(); // Asumiendo que se usa un ID incremental
                string mCommandText = $"INSERT INTO Archivo (Archivo_Id, Archivo_Nombre, Directorio_Id, Archivo_Tamano) VALUES ({pArchivo.Id}, '{pArchivo.Nombre}', {pArchivo.DirectorioId}, {pArchivo.Tamano})";
                return new DAO().ExecuteNonQuery(mCommandText);
            }
            else // ya existe, modificar
            {
                string mCommandText = $"UPDATE Archivo SET Archivo_Nombre = '{pArchivo.Nombre}',Directorio_Id = {pArchivo.DirectorioId}, Archivo_Tamano = {pArchivo.Tamano}  WHERE Archivo_Id = {pArchivo.Id}";
                return new DAO().ExecuteNonQuery(mCommandText);
            }
        }

        public static void Eliminar(Archivo pArchivo)
        {
            string mCommandText = $"DELETE FROM Archivo WHERE Archivo_Id = {pArchivo.Id}";
            new DAO().ExecuteNonQuery(mCommandText);
        }

        public static List<Archivo> Listar(int pDirectorioId)
        {
            string mCommandText = "SELECT * FROM Archivo WHERE Directorio_Id = " + pDirectorioId;
            DataSet mDs = new DAO().ExecuteDataSet(mCommandText);
            List<Archivo> listArchivos = new List<Archivo>();
            foreach (DataRow mDr in mDs.Tables[0].Rows)
            {
                Archivo pArchivo = new Archivo();
                ValorizarEntidad(pArchivo, mDr);
                listArchivos.Add(pArchivo);
            }
            return listArchivos;
        }

        public static Archivo Obtener(int pId, int pDirectorioId)
        {
            string mCommandText = $"SELECT * FROM Archivo WHERE Archivo_Id = {pId} AND Directorio_Id = {pDirectorioId}";
            DataSet mDs = new DAO().ExecuteDataSet(mCommandText);
            if (mDs.Tables.Count > 0 && mDs.Tables[0].Rows.Count > 0)
            {
                Archivo mArchivo = new Archivo();
                ValorizarEntidad(mArchivo, mDs.Tables[0].Rows[0]);
                return mArchivo;
            }
            else
            {
                return null;
            }
        }

        #endregion Métodos públicos

        public static Archivo ObtenerPorNombre(string pArchivo, int pDirectorioId)
        {
            string mCommandText = $"SELECT * FROM Archivo WHERE Archivo_Nombre = '{pArchivo}' AND Directorio_Id = {pDirectorioId}";
            DataSet mDs = new DAO().ExecuteDataSet(mCommandText);
            if (mDs.Tables.Count > 0 && mDs.Tables[0].Rows.Count > 0)
            {
                Archivo mArchivo = new Archivo();
                ValorizarEntidad(mArchivo, mDs.Tables[0].Rows[0]);
                return mArchivo;
            }
            else
            {
                return null;
            }
        }
    }
}