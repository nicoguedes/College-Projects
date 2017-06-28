/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package gestaoacademica.dominio;

import java.sql.Date;

public class Aluno {
    public Aluno(int codigo, String nome, Date dataNascimento, int curso) {
        this.codigo = codigo;
        this.nome = nome;
        this.dataNascimento = dataNascimento;
        this.setCurso(curso);
    }
    
    
    private int codigo;
    private String nome;
    private int curso;

    public int getCurso() {
        return curso;
    }

    public void setCurso(int curso) {
        this.curso = curso;
    }
    private Date dataNascimento;

    public int getCodigo() {
        return codigo;
    }

    public Date getDataNascimento() {
        return dataNascimento;
    }

    public String getNome() {
        return nome;
    }

    public void setCodigo(int codigo) {
        this.codigo = codigo;
    }

    public void setDataNascimento(Date dataNascimento) {
        this.dataNascimento = dataNascimento;
    }

    public void setNome(String nome) {
        this.nome = nome;
    }
}
