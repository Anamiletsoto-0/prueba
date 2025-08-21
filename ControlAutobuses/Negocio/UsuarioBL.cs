using System;
using ControlAutobuses.Datos;
using ControlAutobuses.Entidades;

namespace ControlAutobuses.Negocio
{
    public class UsuarioBL
    {
        private readonly UsuarioRepository usuarioRepository;

        public UsuarioBL()
        {
            usuarioRepository = new UsuarioRepository();
        }

        public Usuario Autenticar(string nombreUsuario, string contrasena)
        {
            // Validaciones de negocio
            if (string.IsNullOrEmpty(nombreUsuario))
                throw new Exception("El nombre de usuario es requerido");

            if (string.IsNullOrEmpty(contrasena))
                throw new Exception("La contraseña es requerida");

            if (nombreUsuario.Length < 3)
                throw new Exception("El usuario debe tener al menos 3 caracteres");

            if (contrasena.Length < 4)
                throw new Exception("La contraseña debe tener al menos 4 caracteres");

            // Llamar al repository para autenticar
            return usuarioRepository.Autenticar(nombreUsuario, contrasena);
        }

        public bool CambiarContrasena(int usuarioId, string nuevaContrasena)
        {
            // Validar nueva contraseña
            if (string.IsNullOrEmpty(nuevaContrasena) || nuevaContrasena.Length < 6)
                throw new Exception("La nueva contraseña debe tener al menos 6 caracteres");

            // Lógica adicional de negocio podría ir aquí
            return usuarioRepository.CambiarContrasena(usuarioId, nuevaContrasena);
        }
    }
}
