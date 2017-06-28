/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package gestaoacademica.iu;

import gestaoacademica.controller.AlunoController;
import gestaoacademica.dominio.Aluno;
import gestaoacademica.dominio.Matricula;
import gestaoacademica.persistencia.MatriculaDAO;
import java.util.ArrayList;
import java.util.Scanner;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author Vinicius
 */
public class Principal {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        try {
            System.out.println("Digite o código do aluno");
            Matricula mat = new Matricula();
            Scanner sc = new Scanner(System.in);
            mat.setAluno(sc.nextInt());

            Aluno aluno = AlunoController.getInstance().get(mat.getAluno());
            
            if (aluno == null){
                System.out.println("Aluno não encontrado.");
            }else{
                System.out.println("Aluno localizado. Nome: " + aluno.getNome() + " Curso: " + aluno.getCurso());

                mat.setCurso(aluno.getCurso());

                mat.setIsMatriculaProcessada(false);

                ArrayList<Integer> disciplinas = new ArrayList<Integer>();
                while (true)
                {
                    System.out.println("Digite o código da disciplina. -1 para sair");
                    int dc = sc.nextInt();
                    if (dc < 0)
                        break;

                    disciplinas.add(dc);
                }

                mat.setDisciplinas(disciplinas);

                MatriculaDAO.getInstance().solicitarMatricula(mat);
                
                System.out.println("Matrícula solicitada com sucesso! Código gerado para matrícula: " + mat.getCodigo());
            }
        } catch (Exception ex) {
            Logger.getLogger(Principal.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
}
