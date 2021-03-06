﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iugu.Lib
{
    /// <summary>
    /// Os clientes efetuam pagamentos através das faturas. 
    /// As faturas contém itens que representam o que o cliente está pagando, o serviço ou produto.
    /// </summary>
    public class Invoice : APIResource
    {
        public Invoice()
        {
            BaseURI += "/invoices";
        }


        //limit (opcional)	Máximo de registros retornados
        //start (opcional)	Quantos registros pular do início da pesquisa (muito utilizado para paginação)
        //created_at_from (opcional)	Registros criados a partir desta data passada no parâmetro
        //created_at_to (opcional)	Registros criados até esta data passada no parâmetro
        //query (opcional)	Neste parâmetro pode ser passado um texto para pesquisa
        //updated_since (opcional)	Registros atualizados desde o valor passado no parâmetro
        //sortBy (opcional)	Um hash sendo a chave o nome do campo para ordenação e o valor sendo DESC ou ASC para descendente e ascendente, respectivamente
        //customer_id (opcional)	ID do Cliente
        public InvoiceListModel Get()
        {
            //TODO: Implementar GET com parametros
            var retorno = GetAsync<InvoiceListModel>().Result;
            return retorno;
        }

        public InvoiceModel Get(string id)
        {
            var retorno = GetAsync<InvoiceModel>(id).Result;
            return retorno;
        }

        /// <summary>
        /// Cria uma Fatura para um Cliente (Podendo ser um objeto cliente ou apenas um e-mail).
        /// </summary>
        /// <param name="email">E-Mail do cliente</param>
        /// <param name="due_date">Data de Expiração (DD/MM/AAAA)</param>
        /// <param name="items"> Itens da Fatura</param>
        /// <param name="return_url">(opcional)	Cliente é redirecionado para essa URL após efetuar o pagamento da Fatura pela página de Fatura da Iugu</param>
        /// <param name="expired_url">(opcional) Cliente é redirecionado para essa URL se a Fatura que estiver acessando estiver expirada</param>
        ///  <param name="notification_url">(opcional) URL chamada para todas as notificações de Fatura, assim como os webhooks (Gatilhos) são chamados</param>
        /// <param name="tax_cents">(opcional) Valor dos Impostos em centavos</param>
        /// <param name="discount_cents">(opcional)	Valor dos Descontos em centavos</param>
        /// <param name="customer_id">(opcional) ID do Cliente</param>
        /// <param name="ignore_due_email">(opcional) Booleano que ignora o envio do e-mail de cobrança</param>
        /// <param name="subscription_id">(opcional) Amarra essa Fatura numa Assinatura</param>
        /// <param name="credits">(opcional) Caso tenha o subscription_id, pode-se enviar o número de créditos a adicionar nessa Assinatura quando a Fatura for paga</param>
        /// <param name="logs">(opcional) Logs da Fatura</param>
        /// <param name="custom_variables">(opcional) Variáveis Personalizadas</param>
        /// <returns></returns>
        public InvoiceModel Create(string email, DateTime due_date, Item[] items, string return_url = "",
            string expired_url = "", string notification_url = "", int tax_cents = 0, int discount_cents = 0, string customer_id = "", bool ignore_due_email = false,
            string subscription_id = "", int credits = 0, Logs logs = null, CustomVariables custom_variables = null)
        {
            //TODO: implementar  custom_variables[]
            var invoice = new
            {
                email = email,
                due_date = due_date.ToString("dd/MM/yyyy"),
                items = items,
                return_url = return_url,
                expired_url = expired_url,
                tax_cents = tax_cents,
                discount_cents = discount_cents,
                customer_id = customer_id,
                ignore_due_email = ignore_due_email,
                subscription_id = subscription_id,
                credits = credits,
                logs = logs,
                custom_variables = custom_variables
            };
            var retorno = PostAsync<InvoiceModel>(invoice).Result;
            return retorno;
        }

        public InvoiceModel Delete(string id)
        {
            var retorno = DeleteAsync<InvoiceModel>(id).Result;
            return retorno;
        }

        public InvoiceModel Put(string id, InvoiceModel model)
        {
            var retorno = PutAsync<InvoiceModel>(id, model).Result;
            return retorno;
        }

        public InvoiceModel Refund(string id)
        {
            BaseURI += string.Format("{0}/refund",id);
            var retorno = PostAsync<InvoiceModel>(null).Result;
            return retorno;
        }

        public InvoiceModel Cancel(string id)
        {
            BaseURI += "/cancel";
            var retorno = PutAsync<InvoiceModel>(id, null).Result;
            return retorno;
        }
    }
}
