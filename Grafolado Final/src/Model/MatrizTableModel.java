package Model;

import javax.swing.table.AbstractTableModel;

/**
 * Um table model para exibir matrizes de inteiros.
 * 
 * @author Everton
 */
public class MatrizTableModel extends AbstractTableModel {
	
	/** Uma refer�ncia para nossa matriz. */
	private int[][] matriz;

	/**
	 * Constr�i um novo modelo para a matriz. Altera��es na matrix original
	 * refletir�o no modelo e vice-versa. Para melhores resultados, use o m�todo
	 * setValue(int, int, int) para alterar a matriz, ao inv�s de altera-la
	 * diretamente.
	 * 
	 * @param matrix
	 *            A matriz a ser usada na tabela.
	 */
	public MatrizTableModel(int[][] matriz) {
		if (matriz == null)
			throw new IllegalArgumentException("Matriz inv�lida!");

		this.matriz = matriz;
	}

	/**
	 * Nesse m�todo, temos que dizer para a tabela o n�mero de colunas de nossa
	 * matriz. Estou assumindo que � uma matriz quadrada, portanto, o n�mero de
	 * colunas ser� o mesmo para todas as linhas.
	 */
	public int getColumnCount() {
		return matriz[0].length;
	}

	/**
	 * Nesse m�todo, temos que dizer para a tabela o n�mero de linhas de nossa
	 * matriz.
	 */
	public int getRowCount() {
		return matriz.length;
	}

	/**
	 * Esse m�todo � chamado pelo JTable, quando ele est� desenhando a matriz.
	 * Para cada c�lula, o table pergunta qual valor deve ser colocado na
	 * c�lula.
	 */
	public Object getValueAt(int row, int col) {
		return Integer.valueOf(matriz[row][col]);
	}	

	/**
	 * Aqui dizemos a tabela qual o nome de cada coluna. No nosso caso nao usamos esse m�todo pois as colunas seguem o default utilizando a ordem Alfabetica
	 * 
	 */
	/*@Override
	public String getColumnName(int col) {
		return Integer.toString(col);
	}*/
}
