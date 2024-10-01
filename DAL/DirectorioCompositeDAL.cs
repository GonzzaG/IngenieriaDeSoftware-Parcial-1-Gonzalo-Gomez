using BEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DirectorioCompositeDAL
    {
        #region Métodos privados
        private static void ValorizarEntidad(DirectorioComposite pDirectorio, DataRow pDr)
        {
            pDirectorio.Id = (int)pDr["Directorio_Id"];
            pDirectorio.Nombre = pDr["Directorio_Nombre"].ToString();
            pDirectorio.PadreId = pDr["Padre_Id"] as int?; // puede ser null por la agregacion 0..* 
        }
        #endregion

        #region Métodos basicos Guardar, Eliminar, Listar, Obtener
        public static int Guardar(DirectorioComposite pDirectorio)
        {
            if (pDirectorio.Id == 0) // no existe, Insertar
            {
                pDirectorio.Id = new DAO().ObtenerUltimoId("Directorio", "Directorio_Id") + 1;
                string mCommandText = $"INSERT INTO Directorio (Directorio_Id, Directorio_Nombre, Padre_Id) VALUES ({pDirectorio.Id}, '{pDirectorio.Nombre}', {pDirectorio.PadreId})";
                return new DAO().ExecuteNonQuery(mCommandText);
            }
            else // ya existe, modificar
            {
                string mCommandText = $"UPDATE Directorio SET Directorio_Nombre = '{pDirectorio.Nombre}', Padre_Id = {pDirectorio.PadreId} WHERE Directorio_Id = {pDirectorio.Id}";
                return new DAO().ExecuteNonQuery(mCommandText);
            }
        }

        public static void Eliminar(DirectorioComposite pDirectorio)
        {
            string mCommandText = $"DELETE FROM Directorio WHERE Directorio_Id = {pDirectorio.Id}";
            new DAO().ExecuteNonQuery(mCommandText);
        }

        public static List<DirectorioComposite> Listar(int pPadreId)
        {
            string mCommandText = "SELECT * FROM Directorio WHERE Padre_Id = " + pPadreId;
            DataSet mDs = new DAO().ExecuteDataSet(mCommandText);
            List<DirectorioComposite> listDirectorios = new List<DirectorioComposite>();
            foreach (DataRow mDr in mDs.Tables[0].Rows)
            {
                DirectorioComposite pDirectorio = new DirectorioComposite();
                ValorizarEntidad(pDirectorio, mDr);
                listDirectorios.Add(pDirectorio);
            }
            return listDirectorios;
        }

        public static DirectorioComposite Obtener(int pId, int pPadreId)
        {
            string mCommandText = $"SELECT * FROM Directorio WHERE Directorio_Id = {pId} AND Padre_Id = {pPadreId}";
            DataSet mDs = new DAO().ExecuteDataSet(mCommandText);
            if (mDs.Tables.Count > 0 && mDs.Tables[0].Rows.Count > 0)
            {
                DirectorioComposite mDirectorio = new DirectorioComposite();
                ValorizarEntidad(mDirectorio, mDs.Tables[0].Rows[0]);
                return mDirectorio;
            }
            else
            {
                return null;
            }
        }
        #endregion

        // Se obtiene un directorio por su nombre y segun el id del padre, osea en el contexto en el que se encuentra
        public static DirectorioComposite ObtenerPorNombre(string pNombredirectorio, int pDirectorioActualId)
        {
            string mCommandText = $"SELECT * FROM Directorio WHERE Directorio_Nombre = {pNombredirectorio} AND Padre_Id = {pDirectorioActualId}";
            DataSet mDs = new DAO().ExecuteDataSet(mCommandText);
            if (mDs.Tables.Count > 0 && mDs.Tables[0].Rows.Count > 0)
            {
                DirectorioComposite mDirectorio = new DirectorioComposite();
                ValorizarEntidad(mDirectorio, mDs.Tables[0].Rows[0]);
                return mDirectorio;
            }
            else
            {
                return null;
            }
        }
       


    }
}
