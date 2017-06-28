/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package gestaoacademica.dominio;

import java.util.ArrayList;

public class Curso {
    private int codigo;
    private String nome;
    private ArrayList<Disciplina> disciplinas;

    public Curso(int codigo, String nome, ArrayList<Disciplina> disciplinas) {
        this.codigo = codigo;
        this.nome = nome;
        this.disciplinas = disciplinas;
    }

    public int getCodigo() {
        return codigo;
    }

    public void setCodigo(int codigo) {
        this.codigo = codigo;
    }

    public ArrayList<Disciplina> getDisciplinas() {
        return disciplinas;
    }

    public void setDisciplinas(ArrayList<Disciplina> disciplinas) {
        this.disciplinas = disciplinas;
    }

    public String getNome() {
        return nome;
    }

    public void setNome(String nome) {
        this.nome = nome;
    }
    
    
}
