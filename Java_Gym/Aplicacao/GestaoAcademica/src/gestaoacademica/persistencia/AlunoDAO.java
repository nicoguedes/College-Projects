/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package gestaoacademica.persistencia;

import gestaoacademica.dominio.Aluno;
import java.sql.SQLException;
import java.util.ArrayList;
import java.sql.Date;
import java.sql.ResultSet;

/**
 *
 * @author Vinicius
 */
public class AlunoDAO {
    private static ArrayList<Aluno> alunosDB;
    
    private static AlunoDAO instancia;
    
    public static AlunoDAO getInstance()
    {
        if (instancia == null)
            instancia = new AlunoDAO();
        
        return instancia;
    }
    
    public AlunoDAO()
    {
        
    }
    
    public Aluno get(int codigo) throws Exception
    {
        String sql = "SELECT * FROM Aluno WHERE Codigo = " + codigo;
		
	ResultSet result = Conexao.getInstance().executeQuery(sql);
		
	if (result.next())
            return resultSetToAluno(result);
		
	return null;
    }
    
    private Aluno resultSetToAluno(ResultSet result) throws SQLException
    {
        return new Aluno(result.getInt("Codigo"), result.getString("Nome"), Date.valueOf(result.getString("DataNascimento")), result.getInt("Curso"));
    }
    
    public ArrayList<Aluno> getAll(){
        return alunosDB;
    }
}
