package VIEW;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;

import javax.imageio.ImageIO;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JMenu;
import javax.swing.JMenuBar;
import javax.swing.JMenuItem;
import javax.swing.JPanel;
import javax.swing.JTextField;

import Model.Arquivo;
import Model.Grafo;
import Utils.JImagePanel;
import Utils.SwingUtils;
import Utils.JImagePanel.FillType;

/**
 * @author everton
 * Um JFrame que irá ser nosso Frame Principal
 */
/**
 * @author everton.gomes
 *
 */
/**
 * @author everton.gomes
 *
 */
public class Principal extends JFrame implements SwingUtils {

	ModalMatriz modalMatriz;
	
	/**
	 * Método main responsavel por startar a aplicação.
	 * @param args - Este parametro só poderá ser utilizado se o programa for executado via prompt de comando.
	 */
	public static void main(String[] args) {
		
		try {
			
			new Principal().setVisible(true);
			
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
	}
	
	Principal() throws IOException{
		
		/*
		 * Definimos o nome da Barra de Titulo
		 */
		this.setTitle("Grafolados");		
		
		this.pack();
		
		/*
		 * Definimos que o nosso JFrame vai abrir em tela cheia em qualquer resolução.
		 */
		this.setExtendedState(MAXIMIZED_BOTH);	
		
		/*
		 * Inicializamos os Componentes do JFrame
		 */
		this.inicializaComponentes();
				
	}
	
	/**
	 * Esse método é utilizado para criar os componentes do JFrame 
	 * @throws IOException 
	 */
	private void inicializaComponentes() throws IOException{			
		
		/*
		 * Apontamos a imagem para um BufferedImage
		 */
		BufferedImage imagem = ImageIO.read(new File(getClass().getResource("/images/puc.jpg").getPath()));
		
		/*
		 * Criamos um JPanel com á imagem á cima.
		 */
		JImagePanel panelImage = new JImagePanel(imagem);
		panelImage.setFillType(FillType.RESIZE);
		
		/*
		 * Setamos ao JFrame o JPanel criado á cima
		 */
		this.setContentPane(panelImage);
		
		/*
		 * Cria a barra de Menus
		 */			
		JMenuBar barraDeMenu = new JMenuBar();	
		
		/*
		 * Cria o menu Principal
		 */
		JMenu menu = getMenu("Editor de Grafos");
		
		/*
		 * Cria o item de menu "Matriz de Adjacências"
		 */
		JMenuItem menuItemMatriz = getMenuItem("Matriz de Adjacências");
		
		/*
		 * Criamos o evento do item de menu "Matriz de Adjacências" esse evento é disparado quando clicarmos no item de menu
		 */
		menuItemMatriz.addActionListener(new ActionListener() {
			
			@Override
			public void actionPerformed(ActionEvent arg0) {
				// TODO Auto-generated method stub
				
				/*
				 * Criamos um objeto Arquivo
				 */
				Arquivo arquivo = new Arquivo();				
				
				/*
				 * Instanciamos o nosso JDialog e passamos para o Construtor a instancia corrente do nosso JFrame
				 */
				Principal.this.modalMatriz = new ModalMatriz(Principal.this);				
				
				try {
					
					/*
					 * Instanciamos a classe grafo e passamos para seu construtor uma matriz de adjacencias booleana retornada pelo método
					 * getMatrizAdjacencia do objeto arquivo.
					 */
					Grafo grafo = new Grafo(arquivo.getMatrizAdjacencia());					
					
					/*
					 * Atribuimos á matrizBoolean á matriz de adjacencia do objeto grafo
					 */
					boolean[][] matrizBoolean = grafo.getGrafoAdjacencias();
					
					/*
					 * Criamos uma matriz de inteiros que irá ser exibida para o usuário
					 */
					int[][] matriz = new int[matrizBoolean.length][matrizBoolean[0].length];
					
					/*
					 * Preenchemos nossa matrizDeAdjacencias em inteiros.
					 */
					for(int linha = 0; linha < matrizBoolean.length; linha ++){
						for(int coluna = 0; coluna < matrizBoolean[0].length; coluna++ ){
							if(matrizBoolean[linha][coluna]){
								matriz[linha][coluna] = 1;
							}else{
								matriz[linha][coluna] = 0;
							}
						}
					}					
					
					/*
					 * Setamos á matriz de adjacencias de inteiros para o objeto modalMatriz
					 */
					modalMatriz.setMatriz(matriz);
					
					/*
					 * Exibimos o JDialog Modal Matriz
					 */
					modalMatriz.setVisible(true);
					
				} catch (IOException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
							
				
			}
		});
		
		/*
		 * Cria o item de menu "Lista Encadeada"
		 */
		JMenuItem menuItemLista = getMenuItem("Lista Encadeada");
		
		/*
		 * adiciona ao menu principal os itens de menu
		 */
		menu.add(menuItemMatriz);
		menu.add(menuItemLista);
		
		/*
		 * Adiciona á barra de menu o menu principal
		 */
		barraDeMenu.add(menu);
		
		/*
		 * Adiciona ao Frame minha barra de Menu
		 */
		this.setJMenuBar(barraDeMenu);
		
	}
	
	@Override
	public JButton getJButton(String nome){
		
		JButton jButton = new JButton(nome);
		
		return jButton;
		
	}
	
	/*
	 * Abaixo todos os métodos herdados da interface SwingUtils
	 */
	
	@Override
	public JPanel getJPanel(){
		
		return new JPanel();
		
	}	
	
	private JMenu getMenu(String nome){
		
		return new JMenu(nome);
		
	}	


	@Override
	public JLabel getJLabel(String nome) {
		// TODO Auto-generated method stub
		return new JLabel(nome);
	}

	@Override
	public JMenu getJMenu(String nome) {
		// TODO Auto-generated method stub
		return new JMenu(nome);
	}

	@Override
	public JTextField getJTextField() {
		// TODO Auto-generated method stub
		return new JTextField();
	}

	@Override
	public JMenuItem getMenuItem(String nome) {
		// TODO Auto-generated method stub
		return new JMenuItem(nome);
	}

}
