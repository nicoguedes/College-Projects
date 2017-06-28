package Utils;

import javax.swing.JButton;
import javax.swing.JLabel;
import javax.swing.JMenu;
import javax.swing.JMenuItem;
import javax.swing.JPanel;
import javax.swing.JTextField;

/**
 * @author everton
 * Interface que cont�m os m�todos responsaveis por retornarem os componentes b�sicos dos JFrame e JDialogs
 */
public interface  SwingUtils {

	public JButton getJButton(String nome);		
	
	public JPanel getJPanel();		
	
	public JMenu getJMenu(String nome);	
	
	public JMenuItem getMenuItem(String nome);
		
	public JLabel getJLabel(String nome);
	
	public JTextField getJTextField();
	
	
}
