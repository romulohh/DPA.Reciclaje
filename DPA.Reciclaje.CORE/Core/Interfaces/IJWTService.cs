using DPA.Reciclaje.CORE.Core.Entities;
using DPA.Reciclaje.CORE.Core.Settings;

namespace DPA.Reciclaje.CORE.Core.Interfaces
{
    public interface IJWTService
    {
        JWTSettings _settings { get; }

        string GenerateJWToken(Usuario user);
    }
}