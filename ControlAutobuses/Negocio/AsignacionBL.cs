using System;
using System.Collections.Generic;
using ControlAutobuses.Datos;
using ControlAutobuses.Entidades;

namespace ControlAutobuses.Negocio
{
    public class AsignacionBL
    {
        private readonly AsignacionRepository asignacionRepository;
        private readonly ChoferRepository choferRepository;
        private readonly AutobusRepository autobusRepository;
        private readonly RutaRepository rutaRepository;

        public AsignacionBL()
        {
            asignacionRepository = new AsignacionRepository();
            choferRepository = new ChoferRepository();
            autobusRepository = new AutobusRepository();
            rutaRepository = new RutaRepository();
        }

        public bool CrearAsignacion(Asignacion asignacion)
        {
            // Validar que todos los IDs sean válidos
            if (asignacion.ChoferId <= 0 || asignacion.AutobusId <= 0 || asignacion.RutaId <= 0)
                throw new Exception("Todos los elementos (chofer, autobús, ruta) deben ser seleccionados");

            // Validar disponibilidad del chofer
            if (!choferRepository.EstaDisponible(asignacion.ChoferId))
                throw new Exception("El chofer seleccionado no está disponible");

            // Validar disponibilidad del autobús
            if (!autobusRepository.EstaDisponible(asignacion.AutobusId))
                throw new Exception("El autobús seleccionado no está disponible");

            // Validar disponibilidad de la ruta
            if (!rutaRepository.EstaDisponible(asignacion.RutaId))
                throw new Exception("La ruta seleccionada no está disponible");

            // Verificar si el chofer ya tiene una asignación activa
            if (asignacionRepository.ChoferTieneAsignacionActiva(asignacion.ChoferId))
                throw new Exception("El chofer ya tiene una asignación activa");

            // Verificar si el autobús ya tiene una asignación activa
            if (asignacionRepository.AutobusTieneAsignacionActiva(asignacion.AutobusId))
                throw new Exception("El autobús ya tiene una asignación activa");

            // Verificar si la ruta ya tiene una asignación activa
            if (asignacionRepository.RutaTieneAsignacionActiva(asignacion.RutaId))
                throw new Exception("La ruta ya tiene una asignación activa");

            // Crear la asignación
            bool resultado = asignacionRepository.Crear(asignacion);

            if (resultado)
            {
                // Marcar los recursos como no disponibles
                choferRepository.MarcarComoNoDisponible(asignacion.ChoferId);
                autobusRepository.MarcarComoNoDisponible(asignacion.AutobusId);
                rutaRepository.MarcarComoNoDisponible(asignacion.RutaId);
            }

            return resultado;
        }

        public bool FinalizarAsignacion(int asignacionId)
        {
            // Obtener la asignación
            Asignacion asignacion = asignacionRepository.ObtenerPorId(asignacionId);

            if (asignacion == null)
                throw new Exception("La asignación no existe");

            // Finalizar la asignación
            bool resultado = asignacionRepository.Finalizar(asignacionId);

            if (resultado)
            {
                // Liberar los recursos (marcar como disponibles)
                choferRepository.MarcarComoDisponible(asignacion.ChoferId);
                autobusRepository.MarcarComoDisponible(asignacion.AutobusId);
                rutaRepository.MarcarComoDisponible(asignacion.RutaId);
            }

            return resultado;
        }

        public List<Asignacion> ObtenerTodasAsignaciones()
        {
            return asignacionRepository.ObtenerTodas();
        }

        public List<Asignacion> ObtenerAsignacionesActivas()
        {
            return asignacionRepository.ObtenerActivas();
        }

        public List<Chofer> ObtenerChoferesDisponibles()
        {
            return choferRepository.ObtenerDisponibles();
        }

        public List<Autobus> ObtenerAutobusesDisponibles()
        {
            return autobusRepository.ObtenerDisponibles();
        }

        public List<Ruta> ObtenerRutasDisponibles()
        {
            return rutaRepository.ObtenerDisponibles();
        }

        public Asignacion ObtenerAsignacionPorId(int id)
        {
            return asignacionRepository.ObtenerPorId(id);
        }

        public List<Asignacion> ObtenerAsignacionesPorChofer(int choferId)
        {
            return asignacionRepository.ObtenerPorChofer(choferId);
        }

        public List<Asignacion> ObtenerAsignacionesPorAutobus(int autobusId)
        {
            return asignacionRepository.ObtenerPorAutobus(autobusId);
        }

        public List<Asignacion> ObtenerAsignacionesPorRuta(int rutaId)
        {
            return asignacionRepository.ObtenerPorRuta(rutaId);
        }
    }
}