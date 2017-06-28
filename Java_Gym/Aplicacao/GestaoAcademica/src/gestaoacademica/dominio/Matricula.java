/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package gestaoacademica.dominio;

import java.util.ArrayList;

public class Matricula {
    private int codigo;

    public int getCodigo() {
        return codigo;
    }

    public void setCodigo(int codigo) {
        this.codigo = codigo;
    }
    private int aluno;
    private ArrayList<Integer> disciplinas;
    private int curso;

    public Matricula()
    {
        
    }
    
    public int getCurso() {
        return curso;
    }

    public void setCurso(int curso) {
        this.curso = curso;
    }
    
    
    public ArrayList<Integer> getDisciplinas() {
        return disciplinas;
    }

    public void setDisciplinas(ArrayList<Integer> disciplinas) {
        this.disciplinas = disciplinas;
    }
    private Boolean isMatriculaProcessada;

    public Matricula(int aluno, Boolean isMatriculaProcessada) {
        setAluno(aluno);
        setIsMatriculaProcessada(isMatriculaProcessada);
    }
    
    public int getAluno() {
        return aluno;
    }

    public void setAluno(int aluno) {
        this.aluno = aluno;
    }

    public Boolean getIsMatriculaProcessada() {
        return isMatriculaProcessada;
    }

    public void setIsMatriculaProcessada(Boolean isMatriculaProcessada) {
        this.isMatriculaProcessada = isMatriculaProcessada;
    }
}
