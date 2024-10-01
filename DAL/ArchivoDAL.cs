using BEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ArchivoDAL
    {
        #region Métodos privados
        private static void ValorizarEntidad(Archivo pArchivo, DataRow pDr)
        {
            pArchivo.Id = (int)pDr["Archivo_Id"];
            pArchivo.Nombre = pDr["Archivo_Nombre"].ToString();
            pArchivo.Tamano = (int)pDr["Archivo_Tamano"];
            pArchivo.DirectorioId = (int)pDr["Directorio_Id"];
        }
        #endregion

        #region Métodos públicos
        public static int Guardar(Archivo pArchivo)
        {
            if (pArchivo.Id == 0) // no existe, Insertar
            {
                pArchivo.Id = new DAO().ObtenerUltimoId("Archivo", "Archivo_Id") + 1; // Asumiendo que se usa un ID incremental
                string mCommandText = $"INSERT INTO Archivo (Archivo_Id, Archivo_Nombre, Archivo_Tamano, Directorio_Id) VALUES ({pArchivo.Id}, '{pArchivo.Nombre}', {pArchivo.Tamano}, {pArchivo.DirectorioId})";
                return new DAO().ExecuteNonQuery(mCommandText);
            }
            else // ya existe, modificar
            {
                string mCommandText = $"UPDATE Archivo SET Archivo_Nombre = '{pArchivo.Nombre}', Archivo_Tamano = {pArchivo.Tamano}, Directorio_Id = {pArchivo.DirectorioId} WHERE Archivo_Id = {pArchivo.Id}";
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

        public static Archivo Obtener(int pId, int pPadreId)
        {
            string mCommandText = $"SELECT * FROM Archivo WHERE Archivo_Id = {pId} AND Padre_Id = {pPadreId}";
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
        #endregion

    }
}
