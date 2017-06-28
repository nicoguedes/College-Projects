/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package gestaoacademica.persistencia;

import java.sql.*;
import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;

public class Conexao {
    private static Connection conexao;
    
    public static Statement getInstance() throws ClassNotFoundException, SQLException, Exception
    {
    	if (conexao == null)
    		abrirConexao();
    	
    	return conexao.createStatement();
    }
    
    public static void abrirConexao() throws ClassNotFoundException, SQLException, Exception
    {
        Class.forName("org.sqlite.JDBC");
        conexao = DriverManager.getConnection("jdbc:sqlite:academico.db");
    }
    
    public static void fecharConexao() throws SQLException
    {
        conexao.close();
    }
}
