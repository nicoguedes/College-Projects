/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package gestaoacademica.dominio;

public class Disciplina {
    private int codigo;
    private String nome;
    private int curso;

    public Disciplina(int codigo, String nome, int curso) {
        this.codigo = codigo;
        this.nome = nome;
        this.curso = curso;
    }

    public int getCodigo() {
        return codigo;
    }

    public void setCodigo(int codigo) {
        this.codigo = codigo;
    }

    public int getCurso() {
        return curso;
    }

    public void setCurso(int curso) {
        this.curso = curso;
    }

    public String getNome() {
        return nome;
    }

    public void setNome(String nome) {
        this.nome = nome;
    }
    
    
}
