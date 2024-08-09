using EntregasADomicilioWeb.BusinessLayer;

namespace EntregaADomicilio.Web.BusinessLayer
{
    public class UnitOfWork
    {
        public InformacionBl Informacion { get; }
        public PlatilloBl Platillo { get; }
        public CategoriaBl Categoria { get; }
        public OpinionBl Opinion { get; }

        public UnitOfWork(PlatilloBl platilloBl, CategoriaBl categoria, OpinionBl opinionBl, InformacionBl informacionBl)
        {
            Platillo = platilloBl;
            Categoria = categoria;
            Opinion = opinionBl;
            Informacion = informacionBl;
        }
    }
}