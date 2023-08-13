using System;
using System.Data;

    public class Parametros
    {
        private string _nombre;
        private SqlDbType _type;
        private object _valor;
        private ParameterDirection _direccion;
        private int _tamanio = 0;

        public Parametros()
        {
        }

        public Parametros(string nombre, SqlDbType type, object valor, ParameterDirection direccion)
        {
            _nombre = nombre;
            _type = type;
            _valor = valor;
            _direccion = direccion;
        }

        public Parametros(string nombre, SqlDbType type, object valor, ParameterDirection direccion, int tamanio)
        {
            _nombre = nombre;
            _type = type;
            _valor = valor;
            _direccion = direccion;
            _tamanio = tamanio;
        }

        public string getNombre
        {
            get { return _nombre; }
        }

        public SqlDbType Type
        {
            get { return _type; }
        }

        public object getValor
        {
            get { return _valor; }
        }

        public ParameterDirection getDireccion
        {
            get { return _direccion; }
        }

        public int getTamanio
        {
            get { return _tamanio; }
        }
    }