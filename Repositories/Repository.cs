namespace EntregaADomicilio.Web.Repositories
{
    public class Repository
    {
        public PlatilloRepository Platillo { get; }    
        public CategoriaRepository Categoria{ get; }

        public Repository(PlatilloRepository platillo, CategoriaRepository categoriaRepository)
        {
            Platillo = platillo;
            Categoria = categoriaRepository;    
        }
    }


}