using Microsoft.EntityFrameworkCore;
using Registro_Detalle.DAL;
using Registro_Detalle.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Registro_Detalle.BLL
{
    public class PrestamoMorasBLL
    {

        //Metodo Existe.
        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                ok = contexto.PrestamoMoras.Any(e => e.MoraId == id);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return ok;
        }

        //Metodo Insertar.
        private static bool Insertar(PrestamoMoras prestamoMoras)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                if (contexto.PrestamoMoras.Add(prestamoMoras) != null)
                {
                    ok = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return ok;
        }



        //Metodo Modificar.
        private static bool Modificar(PrestamoMoras prestamoMoras)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                contexto.Database.ExecuteSqlRaw($"Delete FROM MorasDetalle Where MoraId={prestamoMoras.MoraId}");
                //foreach (var anterior in prestamoMoras.PrestamoMorasDetalle)
                {
                    //contexto.Entry(anterior).State = EntityState.Added;
                }
                contexto.Entry(prestamoMoras).State = EntityState.Modified;
                ok = (contexto.SaveChanges() > 0);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return ok;
        }


        //Metodo Guardar.
        public static bool Guardar(PrestamoMoras prestamo)
        {
            if (!Existe(prestamo.MoraId))
            {
                return Insertar(prestamo);
            }
            else
            {
                return Modificar(prestamo);
            }
        }

        //Metodo Buscar
        public static PrestamoMoras Buscar(int id)
        {
            Contexto contexto = new Contexto();
            PrestamoMoras moras = new PrestamoMoras();

            try
            {
              //  moras = contexto.PrestamoMoras.Include(x => x.PrestamoMorasDetalle).Where(p => p.MoraId == id).SingleOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return moras;
        }

        //Metodo Eliminar.
        public static bool Eliminar(int id)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                var eliminar = contexto.PrestamoMoras.Find(id);
                contexto.Entry(eliminar).State = EntityState.Deleted;
                ok = (contexto.SaveChanges() > 0);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return ok;
        }

    }
}
