package Model;

import javax.swing.table.AbstractTableModel;

/**
 * Um table model para exibir matrizes de inteiros.
 * 
 * @author Everton
 */
public class MatrizTableModel extends AbstractTableModel {
	
	/** Uma referência para nossa matriz. */
	private int[][] matriz;

	/**
	 * Constrói um novo modelo para a matriz. Alterações na matrix original
	 * refletirão no modelo e vice-versa. Para melhores resultados, use o método
	 * setValue(int, int, int) para alterar a matriz, ao invés de altera-la
	 * diretamente.
	 * 
	 * @param matrix
	 *            A matriz a ser usada na tabela.
	 */
	public MatrizTableModel(int[][] matriz) {
		if (matriz == null)
			throw new IllegalArgumentException("Matriz inválida!");

		this.matriz = matriz;
	}

	/**
	 * Nesse método, temos que dizer para a tabela o número de colunas de nossa
	 * matriz. Estou assumindo que é uma matriz quadrada, portanto, o número de
	 * colunas será o mesmo para todas as linhas.
	 */
	public int getColumnCount() {
		return matriz[0].length;
	}

	/**
	 * Nesse método, temos que dizer para a tabela o número de linhas de nossa
	 * matriz.
	 */
	public int getRowCount() {
		return matriz.length;
	}

	/**
	 * Esse método é chamado pelo JTable, quando ele está desenhando a matriz.
	 * Para cada célula, o table pergunta qual valor deve ser colocado na
	 * célula.
	 */
	public Object getValueAt(int row, int col) {
		return Integer.valueOf(matriz[row][col]);
	}	

	/**
	 * Aqui dizemos a tabela qual o nome de cada coluna. No nosso caso nao usamos esse método pois as colunas seguem o default utilizando a ordem Alfabetica
	 * 
	 */
	/*@Override
	public String getColumnName(int col) {
		return Integer.toString(col);
	}*/
}
