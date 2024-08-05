namespace EntregaADomicilio.Web.BusinessLayer
{
    public class UnitOfWork
    {
        public PlatilloBl Platillo { get;  }
        public CategoriaBl Categoria { get; }

        public UnitOfWork(PlatilloBl platilloBl, CategoriaBl categoria) 
        {
            Platillo = platilloBl;
            Categoria = categoria;
        }
    }
}