package Model;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.io.InputStream;
import java.net.URL;
import java.util.ArrayList;
import java.util.List;

/**
 * @author everton
 * Classe responsavel por Manipular o Arquivo contendo as matrizes
 */
public class Arquivo {

	private File file;
	
	public boolean[][] getMatrizAdjacencia() throws IOException{			
		
		String path = this.getClass().getResource("/arquivo_grafo/entrada_matriz.txt").getPath();
		
		file = new File(path);		
	
		boolean[][] matrizAdjacencia = null;
		
		try 
		{
			
			FileReader reader = new FileReader(file);
			
			BufferedReader br = new BufferedReader(reader);
			
			String linhaArquivo = "";
			
			List<String> linhasConcatenadas = new ArrayList<String>();			
			
			
			while((linhaArquivo = br.readLine()) != null){
								
				linhasConcatenadas.add(linhaArquivo);
				
			}			
			
			int numeroVertices = Integer.parseInt(linhasConcatenadas.get(0));
			
			matrizAdjacencia = new boolean [numeroVertices][numeroVertices];			
						
			for (int i = 0; i < linhasConcatenadas.size(); i++)
			{
				
				if (i > 0)
				{
					String vetorLinhas[] = linhasConcatenadas.get(i).split(";");					
					
					int linhaMatriz = i - 1;
					
					for(int colunaMatriz = 0; colunaMatriz < numeroVertices; colunaMatriz++){
						//boolean conteudoCelula = false;
						//if (vetorLinhas[colunaMatriz] == "1")
							//conteudoCelula = true;
							matrizAdjacencia[linhaMatriz][colunaMatriz] = (vetorLinhas[colunaMatriz].equals("1")) ? true : false;
							
					}
						
				}
					
			}
			
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		return matrizAdjacencia;
		
	}
	
}
