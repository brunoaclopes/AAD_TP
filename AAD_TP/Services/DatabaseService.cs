using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AAD_TP.Model;
using AAD_TP.ViewModel;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using Npgsql;
using NpgsqlTypes;

namespace AAD_TP.Services
{
    public class DatabaseService
    {
        public DatabaseService() { }

        private readonly string _connectionString = string.Format("Server=127.0.0.1;Port=5432;" + "User Id=admin;Password=admin1234;Database=AAD_TP;");
        private string _command;
        private DataSet _ds = new DataSet();
        private DataTable _dt = new DataTable();


        public IEnumerable<Hospital> GetHospitais()
        {
            _command =
                "SELECT id, nome, tipo, codigo_postal, arruamento, localidade, concelho, distrito from hospital;";

            var npgsqlconnection = new NpgsqlConnection(_connectionString);
            npgsqlconnection.Open();

            var da = new NpgsqlDataAdapter(_command, _connectionString);
            _ds.Reset();
            da.Fill(_ds);

            var hospitais = _ds.Tables[0].AsEnumerable()
                .Select(dataRow => new Hospital(
                    dataRow.Field<int>("id"),
                    dataRow.Field<string>("nome"),
                    dataRow.Field<string>("tipo"),
                    dataRow.Field<string>("codigo_postal"),
                    dataRow.Field<string>("arruamento"),
                    dataRow.Field<string>("localidade"),
                    dataRow.Field<string>("concelho"),
                    dataRow.Field<string>("distrito") )).ToList();

            npgsqlconnection.Close();

            return hospitais;
        }

        public IEnumerable<Professional> GetProfissionais()
        {
            _command =
                "SELECT id, nome, tipo, especialidade, id_hospital from profissional;";

            var npgsqlconnection = new NpgsqlConnection(_connectionString);
            npgsqlconnection.Open();

            var da = new NpgsqlDataAdapter(_command, _connectionString);
            _ds.Reset();
            da.Fill(_ds);
            var profissionais = _ds.Tables[0].AsEnumerable()
                .Select(dataRow => new Professional(
                    dataRow.Field<int>("id"),
                    dataRow.Field<string>("nome"),
                    dataRow.Field<string>("tipo"),
                    dataRow.Field<string>("especialidade"),
                    dataRow.Field<int>("id_hospital") )).ToList();

            npgsqlconnection.Close();

            return profissionais;
        }

        public IEnumerable<Recurso> GetRecursos()
        {
            _command =
                "SELECT r.id, r.nome, tr.nome nome_tipo, r.id_seccao, r.estado from recurso r inner join tipo_recurso tr on r.id_tipo = tr.id;";

            var npgsqlconnection = new NpgsqlConnection(_connectionString);
            npgsqlconnection.Open();

            var da = new NpgsqlDataAdapter(_command, _connectionString);
            _ds.Reset();
            da.Fill(_ds);

            var recursos = _ds.Tables[0].AsEnumerable()
                .Select(dataRow => new Recurso(
                    dataRow.Field<string>("id"),
                    dataRow.Field<string>("nome"),
                    dataRow.Field<string>("nome_tipo"),
                    dataRow.Field<int>("id_seccao"),
                    dataRow.Field<string>("estado") )).ToList();

            npgsqlconnection.Close();

            return recursos;
        }

        public IEnumerable<Tipo_Recurso> GetTipoRecursos()
        {
            _command =
                "SELECT id, nome from tipo_recurso;";

            var npgsqlconnection = new NpgsqlConnection(_connectionString);
            npgsqlconnection.Open();

            var da = new NpgsqlDataAdapter(_command, _connectionString);
            _ds.Reset();
            da.Fill(_ds);

            var tipos = _ds.Tables[0].AsEnumerable()
                .Select(dataRow => new Tipo_Recurso(
                    dataRow.Field<int>("id"),
                    dataRow.Field<string>("nome") )).ToList();

            npgsqlconnection.Close();

            return tipos;
        }

        public IEnumerable<Seccao> GetSeccoes()
        {
            _command =
                "SELECT id, nome from seccao";

            var npgsqlconnection = new NpgsqlConnection(_connectionString);
            npgsqlconnection.Open();

            var da = new NpgsqlDataAdapter(_command, _connectionString);
            _ds.Reset();
            da.Fill(_ds);

            var seccoes = _ds.Tables[0].AsEnumerable()
                .Select(dataRow => new Seccao(
                    dataRow.Field<int>("id"),
                    dataRow.Field<string>("nome"))).ToList();

            npgsqlconnection.Close();

            return seccoes;
        }
        public IEnumerable<Seccao> GetSeccoes(int id)
        {
            _command =
                $"SELECT id, nome from seccao where id_hospital = {id};";

            var npgsqlconnection = new NpgsqlConnection(_connectionString);
            npgsqlconnection.Open();

            var da = new NpgsqlDataAdapter(_command, _connectionString);
            _ds.Reset();
            da.Fill(_ds);

            var seccoes = _ds.Tables[0].AsEnumerable()
                .Select(dataRow => new Seccao(
                    dataRow.Field<int>("id"),
                    dataRow.Field<string>("nome") )).ToList();

            npgsqlconnection.Close();

            return seccoes;
        }

        public IEnumerable<string> GetCompetencias(int id)
        {
            _command =
                $"SELECT tp.nome from tipo_recurso tp inner join competencia c on tp.id = c.id_tipo where c.id_profissional = {id};";

            var npgsqlconnection = new NpgsqlConnection(_connectionString);
            npgsqlconnection.Open();

            var da = new NpgsqlDataAdapter(_command, _connectionString);
            _ds.Reset();
            da.Fill(_ds);

            var competencias = _ds.Tables[0].AsEnumerable()
                .Select(dataRow => dataRow.Field<string>("nome") ).ToList();

            npgsqlconnection.Close();

            return competencias;
        }

        public IEnumerable<Deslocamento> GetDeslocamentos(string id)
        {
            _command =
                $"SELECT id_profissional, id_origem, id_destino from deslocamento where id_recurso = '{id}';";

            var npgsqlconnection = new NpgsqlConnection(_connectionString);
            npgsqlconnection.Open();

            var da = new NpgsqlDataAdapter(_command, _connectionString);
            _ds.Reset();
            da.Fill(_ds);

            var deslocamentos = _ds.Tables[0].AsEnumerable()
                .Select(dataRow => new Deslocamento(
                    dataRow.Field<int>("id_profissional"),
                    dataRow.Field<int>("id_origem"),
                    dataRow.Field<int>("id_destino"))).ToList();

            npgsqlconnection.Close();

            return deslocamentos;
        }

        public IEnumerable<Estado> GetEstados(string id)
        {
            _command =
                $"SELECT data_reparacao, data_entrega from estado where id_recurso = '{id}';";

            var npgsqlconnection = new NpgsqlConnection(_connectionString);
            npgsqlconnection.Open();

            var da = new NpgsqlDataAdapter(_command, _connectionString);
            _ds.Reset();
            da.Fill(_ds);

            var estados = _ds.Tables[0].AsEnumerable()
                .Select(dataRow => new Estado(
                    dataRow.Field<DateTime>("data_reparacao"),
                    dataRow.Field<DateTime>("data_entrega") )).ToList();

            npgsqlconnection.Close();

            return estados;
        }

        public void InsertSeccao(int id, string nome)
        {
            _command = "INSERT INTO seccao ( id_hospital, nome ) VALUES ( @p1, @p2 );";
            
            var npgsqlconnection = new NpgsqlConnection(_connectionString);
            npgsqlconnection.Open();

            using (var cmd = new NpgsqlCommand(_command, npgsqlconnection))
            {
                cmd.Parameters.AddWithValue("p1", NpgsqlDbType.Integer, id);
                cmd.Parameters.AddWithValue("p2", NpgsqlDbType.Varchar, nome);
                cmd.ExecuteNonQuery();
            }

            npgsqlconnection.Close();
        }

        public void InsertProfessional(string nome, string tipo, string especialidade, int id_hospital)
        {
            _command = "INSERT INTO profissional ( nome, tipo, especialidade, id_hospital ) VALUES ( @p1, @p2, @p3, @p4 );";

            var npgsqlconnection = new NpgsqlConnection(_connectionString);
            npgsqlconnection.Open();

            using (var cmd = new NpgsqlCommand(_command, npgsqlconnection))
            {
                cmd.Parameters.AddWithValue("p1", NpgsqlDbType.Varchar, nome);
                cmd.Parameters.AddWithValue("p2", NpgsqlDbType.Varchar, tipo);
                cmd.Parameters.AddWithValue("p3", NpgsqlDbType.Varchar, especialidade);
                cmd.Parameters.AddWithValue("p4", NpgsqlDbType.Integer, id_hospital);
                cmd.ExecuteNonQuery();
            }

            npgsqlconnection.Close();
        }

        public void InsertCompetencia(int profissional, int tipo)
        {
            _command = "INSERT INTO competencia ( id_profissional, id_tipo ) VALUES ( @p1, @p2 );";

            var npgsqlconnection = new NpgsqlConnection(_connectionString);
            npgsqlconnection.Open();

            using (var cmd = new NpgsqlCommand(_command, npgsqlconnection))
            {
                cmd.Parameters.AddWithValue("p1", NpgsqlDbType.Integer, profissional);
                cmd.Parameters.AddWithValue("p2", NpgsqlDbType.Integer, tipo);
                cmd.ExecuteNonQuery();
            }

            npgsqlconnection.Close();
        }

        public void InsertRecurso(string id, string nome, int id_seccao, int id_tipo)
        {
            _command = "INSERT INTO recurso ( id, nome, id_seccao, estado, id_tipo ) VALUES ( @p1, @p2, @p3, @p4, @p5 );";

            var npgsqlconnection = new NpgsqlConnection(_connectionString);
            npgsqlconnection.Open();

            using (var cmd = new NpgsqlCommand(_command, npgsqlconnection))
            {
                cmd.Parameters.AddWithValue("p1", NpgsqlDbType.Varchar, id);
                cmd.Parameters.AddWithValue("p2", NpgsqlDbType.Varchar, nome);
                cmd.Parameters.AddWithValue("p3", NpgsqlDbType.Integer, id_seccao);
                cmd.Parameters.AddWithValue("p4", NpgsqlDbType.Varchar, Estado_Recurso.Funcional.ToString());
                cmd.Parameters.AddWithValue("p5", NpgsqlDbType.Integer, id_tipo);
                cmd.ExecuteNonQuery();
            }

            npgsqlconnection.Close();
        }

        public void InsertDeslocamneto( string id_recurso, int id_professional, int destino)
        {
            var origem = ServiceLocator.Current.GetInstance<MainViewModel>().SelectedRecurso.Id_Seccao;

            _command = "INSERT INTO deslocamento ( id_recurso, id_profissional, id_origem, id_destino ) VALUES ( @p1, @p2, @p3, @p4 );";

            var npgsqlconnection = new NpgsqlConnection(_connectionString);
            npgsqlconnection.Open();

            using (var cmd = new NpgsqlCommand(_command, npgsqlconnection))
            {
                cmd.Parameters.AddWithValue("p1", NpgsqlDbType.Varchar, id_recurso);
                cmd.Parameters.AddWithValue("p2", NpgsqlDbType.Integer, id_professional);
                cmd.Parameters.AddWithValue("p3", NpgsqlDbType.Integer, origem);
                cmd.Parameters.AddWithValue("p4", NpgsqlDbType.Integer, destino);
                cmd.ExecuteNonQuery();
            }

            _command = $"UPDATE recurso SET id_seccao = @p1 where id = '{id_recurso}';";

            using (var cmd = new NpgsqlCommand(_command, npgsqlconnection))
            {
                cmd.Parameters.AddWithValue("p1", NpgsqlDbType.Integer, destino);
                cmd.ExecuteNonQuery();
            }

            npgsqlconnection.Close();
        }

        public void InsertReparacao(string id, DateTime? date)
        {
            try
            {
                _command = "INSERT INTO estado ( id_recurso, data_reparacao, data_entrega ) VALUES ( @p1, @p2, @p3 );";

                var npgsqlconnection = new NpgsqlConnection(_connectionString);
                npgsqlconnection.Open();

                using (var cmd = new NpgsqlCommand(_command, npgsqlconnection))
                {
                    cmd.Parameters.AddWithValue("p1", NpgsqlDbType.Varchar, id);
                    cmd.Parameters.AddWithValue("p2", NpgsqlDbType.Date, DateTime.Now);
                    cmd.Parameters.AddWithValue("p3", NpgsqlDbType.Date, date.Value);
                    cmd.ExecuteNonQuery();
                }

                _command = $"UPDATE recurso SET estado = @p1 where id = '{id}';";

                using (var cmd = new NpgsqlCommand(_command, npgsqlconnection))
                {
                    cmd.Parameters.AddWithValue("p1", NpgsqlDbType.Varchar, "EmReparacao");
                    cmd.ExecuteNonQuery();
                }

                npgsqlconnection.Close();
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show(e.ToString());
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        public void InsertReparacao(string id)
        {
            _command = $"UPDATE recurso SET estado = @p1 where id = '{id}';";

            var npgsqlconnection = new NpgsqlConnection(_connectionString);
            npgsqlconnection.Open();

            using (var cmd = new NpgsqlCommand(_command, npgsqlconnection))
            {
                cmd.Parameters.AddWithValue("p1", NpgsqlDbType.Varchar, "NaoFuncional");
                cmd.ExecuteNonQuery();
            }

            npgsqlconnection.Close();
        }
    }
}
