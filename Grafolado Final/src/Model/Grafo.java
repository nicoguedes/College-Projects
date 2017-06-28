package Model;

/**
 * @author everton
 *Nossa classe que contém todos métodos nescessarios sobre o Grafo
 */
public class Grafo {

	private boolean [][] grafoAdjacencias;
	private int numeroArestas;
	private int numeroVertices;
	
	public Grafo(int numeroVertices)
	{
		grafoAdjacencias = new boolean[numeroVertices][numeroVertices];
	}	
	
	/**
	 * Construtor utilizado para atribuir uma matriz existente de adjacencias
	 * @param grafoAdjacencias, este parametro e recebe uma matriz de adjacencias.
	 */
	public Grafo(boolean [][] grafoAdjacencias  )
	{
		this.grafoAdjacencias = grafoAdjacencias;
		this.numeroVertices = grafoAdjacencias.length;
	}

	public void setNumeroArestas(int numeroArestas) {
		this.numeroArestas = numeroArestas;
	}

	public int getNumeroArestas() {               
		return numeroArestas;
	}

	public void setNumeroVertices(int numeroVertices) {
		this.numeroVertices = numeroVertices;
	}

	public int getNumeroVertices() {
		return numeroVertices;
	}
	
	public boolean[][] getGrafoAdjacencias() {
		return grafoAdjacencias;
	}

	public void setGrafoAdjacencias(boolean[][] grafoAdjacencias) {
		this.grafoAdjacencias = grafoAdjacencias;
	}

	public void inserirAresta(int vertice1, int vertice2)
	{
		this.grafoAdjacencias[vertice1][vertice2] = true;
	}
	
	public void removerAresta(int vertice1, int vertice2)
	{
		this.grafoAdjacencias[vertice1][vertice2] = false;
	}
	
	public int calcularGrau(int vertice)
	{
		int grau = 0;
		for (int i = 0; i < this.getNumeroVertices(); i++)
		{
			if (this.grafoAdjacencias[i][vertice])
				grau++;
		}
		return grau;
	}
	
	public String mostrarMatriz()
	{
		String s = "";
		int numeroVertices = this.getNumeroVertices();
		for (int linha = 0; linha < numeroVertices; linha++)
		{
			s += "\n";
			for (int coluna = 0; coluna < numeroVertices; coluna++)
			{
				s += this.grafoAdjacencias[linha][coluna] + " ";
			}
		}
		return s;
	}
	
	public boolean isGrafoCompleto()
	{
		int numeroVertices = this.getNumeroVertices();
		for (int linha = 0; linha < numeroVertices; linha++)
		{
			for (int coluna = 0; coluna < numeroVertices; coluna++)
			{
				if (!grafoAdjacencias[linha][coluna])
					return false;
			}
		}
		return true;
	}
	
	public boolean isGrafoRegular()
	{
		int numeroVertices = this.getNumeroVertices();
		for (int linha = 0; linha < numeroVertices; linha++)
		{
			for (int coluna = 0; coluna < numeroVertices; coluna++)
			{
				if (linha != coluna)
				{
					if (this.calcularGrau(linha) != this.calcularGrau(coluna))
					{
						return false;
					}
				}
			}
		}
		return true;
	}
	
	public boolean isVerticePendente(int vertice)
	{
		if (this.calcularGrau(vertice) == 1)
			return true;
		else
			return false;
	}
	
	public boolean isVerticeIsolado(int vertice)
	{
		if (this.calcularGrau(vertice) == 0)
			return true;
		else
			return false;
	}
	
	public boolean isGrafoNulo()
	{
		if (this.getNumeroVertices() == 0)
			return true;
		else
			return false;
	}
	
	public boolean isGrafoEuleriano()
	{
		int tamanho = getNumeroVertices();
		
		for (int linha = 0; linha < tamanho; linha++)
		{
			if (this.calcularGrau(linha) % 2 != 0)
				return false;
		}
		
		return true;

	}
	
	public boolean isUnicursal(){
		
		int tamanho = getNumeroVertices();
		
		int contador = 0;
		
		for (int linha = 0; linha < tamanho; linha++)
		{
			if (this.calcularGrau(linha) % 2 != 0)
				contador++;
		}
		
		if(contador != 2){
			
			return false;
			
		}else{
			
			return true;
			
		}			
		
	}
}
