/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package gestaoacademica.persistencia;
import gestaoacademica.dominio.*;
import java.sql.ResultSet;

/**
 *
 * @author Vinicius
 */
public class MatriculaDAO {
    private static MatriculaDAO instancia;
    
    public static MatriculaDAO getInstance()
    {
        if (instancia == null)
            instancia = new MatriculaDAO();
        
        return instancia;
    }
    
    public MatriculaDAO()
    {
        
    }
    
    public void solicitarMatricula(Matricula mat) throws Exception
    {
        String query = "INSERT INTO Matricula (CodigoAluno, MatriculaProcessada, Curso) VALUES (%1$s, '%2$s', %3$s)";
	int matriculaProcessada = mat.getIsMatriculaProcessada() ? 1 : 0;
        String sql = String.format(query, mat.getAluno(), matriculaProcessada, mat.getCurso());
	
        Conexao.getInstance().execute(sql);
        
        mat.setCodigo(getCodigoUltimaMatriculaAluno(mat.getAluno()));
        
        for (int i = 0; i < mat.getDisciplinas().size(); i++){
            
            vincularDisciplina(mat.getDisciplinas().get(i), mat.getCodigo());
        }
    }
    
    private void vincularDisciplina(int disciplina, int matricula) throws Exception
    {
        String query = "INSERT INTO MatriculaDisciplina (CodMatricula, CodigoDisciplina) VALUES (%1$s, %2$s)";
	String sql = String.format(query, matricula, disciplina);
		
	Conexao.getInstance().execute(sql);
    }
    
    private int getCodigoUltimaMatriculaAluno(int aluno) throws Exception
    {
        String sql = "SELECT CodMatricula FROM Matricula  WHERE CodigoAluno = " + aluno + " ORDER BY CodMatricula DESC";
        
        ResultSet result = Conexao.getInstance().executeQuery(sql);
        
        if (result.next())
            return result.getInt("CodMatricula");
        
        
        throw new Exception("Matrícula não cadastrada.");
    }
}
