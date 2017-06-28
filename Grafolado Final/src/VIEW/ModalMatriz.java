package VIEW;

import java.awt.Dimension;
import java.awt.Font;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.JButton;
import javax.swing.JDialog;
import javax.swing.JLabel;
import javax.swing.JMenu;
import javax.swing.JMenuItem;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JTable;
import javax.swing.JTextField;

import net.miginfocom.swing.MigLayout;

import Model.MatrizTableModel;
import Utils.SwingUtils;

/**
 * @author everton
 *JDialog Modal que será acionado pelo JFrame Principal ao clicarmos no item de menu Matriz de Adjacencias
 */
public class ModalMatriz extends JDialog implements SwingUtils{
	
	/*
	 * Matriz de adjacencias exibida no JTable
	 */
	private int[][] matriz;
	
	/*
	 * Componentes do JDialog
	 */
	private JTable jTable;
	
	/**
	 * Construtor inicializado ao clicarmos no item de menu Matriz de Adjacencias
	 * @param principal - Este parametro é utilizado para que nosso JDialog fique modal do JFrame Principal
	 */
	ModalMatriz(Principal principal){
		
		super(principal);
		
		this.setModal(true);		

		init();
		
		this.pack();
		
		this.setSize(new Dimension(600,600));
		
		this.setResizable(false);
		
		this.setLocationRelativeTo(null);		
	}

	
	/**
	 * Método utilizado para iniciar os componentes do JDialog ModalMatriz
	 */
	private void init(){
		
		/*
		 * Cria um JPanel e seta seu layoutManager
		 */
		JPanel jPanel = getJPanel();		
		jPanel.setLayout(new MigLayout());		
		
		/*
		 *Seta ao JDialog seu JPanel Principal que irá conter todos os outros JPanels deste Modal.
		 */
		this.setContentPane(jPanel);
		
		/*
		 * Criamos o JPanel que ira contér o Titulo do Modal ele se encontra na parte superior do JDialog(north)
		 */
		JPanel panelNorth = getJPanel();
		panelNorth.setLayout(new MigLayout());		
		jPanel.add(panelNorth, "dock north, grow, gap left 150");
		
		/*
		 * Criamos um JLabel e definimos sua Fonte
		 */
		JLabel labelInformacoesSobreGrafo = getJLabel("Informações sobre o Grafo");
		labelInformacoesSobreGrafo.setFont(new Font("Verdana", Font.BOLD, 16));
		
		/*
		 * Adicionamos ao JPanel North o JLabel criado á cima
		 */
		panelNorth.add(labelInformacoesSobreGrafo);
		
		JPanel panelEsquerda = getJPanel();
		panelEsquerda.setLayout(new MigLayout());		
		
		JLabel labelNumeroDeVertices = getJLabel("Número de Vertices: ");
		JLabel labelInformacaoDoNumeroDeVertices = getJLabel("teste");
		JLabel labelNumeroDeArestas = getJLabel("Número de Arestas: ");
		JLabel labelInformacaoDoNumeroDeArestas = getJLabel("teste");
		JLabel labelGrafoEuleriano = getJLabel("Grafo Euleriano: ");
		JLabel labelInformacaoGrafoEuleriano = getJLabel("teste"); 
		JLabel labelGrafoUnicursal = getJLabel("Grafo Unicursal: ");
		JLabel labelInformacaoGrafoUnicursal = getJLabel("teste"); 
		JLabel labelGrafoCompleto = getJLabel("Grafo Completo: ");
		JLabel labelInformacaoGrafoCompleto = getJLabel("teste");
		JLabel labelGrafoRegular = getJLabel("Grafo Regular: ");
		JLabel labelInformacaoGrafoRegular = getJLabel("teste"); 
		
		
		panelEsquerda.add(labelNumeroDeVertices);
		panelEsquerda.add(labelInformacaoDoNumeroDeVertices, "wrap");
		panelEsquerda.add(labelNumeroDeArestas);
		panelEsquerda.add(labelInformacaoDoNumeroDeArestas, "wrap");
		panelEsquerda.add(labelGrafoEuleriano);
		panelEsquerda.add(labelInformacaoGrafoEuleriano, "wrap");
		panelEsquerda.add(labelGrafoUnicursal);
		panelEsquerda.add(labelInformacaoGrafoUnicursal, "wrap");
		panelEsquerda.add(labelGrafoCompleto);
		panelEsquerda.add(labelInformacaoGrafoCompleto, "wrap");
		panelEsquerda.add(labelGrafoRegular);
		panelEsquerda.add(labelInformacaoGrafoRegular, "wrap");
		
		jPanel.add(panelEsquerda, "wrap");		
		
		JPanel panelButton = getJPanel();
		panelButton.setLayout(new MigLayout());
		JButton jButtonVisualizarMatriz = getJButton("Visualizar Matriz");
		jButtonVisualizarMatriz.addActionListener(new ActionListener() {
			
			@Override
			public void actionPerformed(ActionEvent e) {
				// TODO Auto-generated method stub
				
				ModalMatriz.this.jTable.setModel(new MatrizTableModel(ModalMatriz.this.matriz));
				
				
			}
		});
		
		panelButton.add(jButtonVisualizarMatriz, "gap left 450");	
		
		
		jTable = new JTable();
		JScrollPane scroll = new JScrollPane();
		scroll.setViewportView(jTable);
		JPanel panelJTable = getJPanel();
		//panelJTable.setBorder(new TitledBorder("teste"));
		panelJTable.setLayout(new MigLayout());
		panelJTable.add(scroll,"w :590:");
		
		jPanel.add(panelJTable, "w :600:, h :110:, wrap");
		jPanel.add(panelButton, "w :600:");
		
		
	}
	
	/*
	 * Abaixo estão os métodos herdados da interface SwingUtils.
	 */
	
	@Override
	public JButton getJButton(String nome) {
		// TODO Auto-generated method stub
		return new JButton(nome);
	}

	@Override
	public JPanel getJPanel() {
		// TODO Auto-generated method stub
		return new JPanel();
	}

	@Override
	public JMenu getJMenu(String nome) {
		// TODO Auto-generated method stub
		return new JMenu();
	}

	@Override
	public JMenuItem getMenuItem(String nome) {
		// TODO Auto-generated method stub
		return new JMenuItem();
	}

	@Override
	public JLabel getJLabel(String nome) {
		// TODO Auto-generated method stub
		return new JLabel(nome);
	}

	@Override
	public JTextField getJTextField() {
		// TODO Auto-generated method stub
		return new JTextField();
	}


	public int[][] getMatriz() {
		return matriz;
	}


	public void setMatriz(int[][] matriz) {
		this.matriz = matriz;
	}	
	
}
