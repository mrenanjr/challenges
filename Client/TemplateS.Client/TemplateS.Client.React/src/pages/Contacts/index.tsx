/* eslint-disable react-hooks/exhaustive-deps */
/* eslint-disable jsx-a11y/anchor-is-valid */
import { useEffect, useState } from 'react';
import { useHistory } from 'react-router-dom';
import { FiChevronRight, FiTrash2 } from 'react-icons/fi';
import { MdOutlineContactMail, MdOutlineContacts } from 'react-icons/md';

import { Header, HeaderContent, ContactsContent, SubHeader, Title } from './styles';

import { useAuth } from '../../hooks/auth';

import api from '../../services/api';
import moment from 'moment';
import { useToast } from '../../hooks/toast';

export interface IContact {
    id: string;
    email: string;
    phone: string;
    whatsapp: string;
    createddate: Date;
    name: string;
}

const Contacts: React.FC = () => {
  const history = useHistory();
  const { user } = useAuth();
  const { addToast } = useToast();

  const [contacts, setContacts] = useState<IContact[]>([]);

  useEffect(() => {
    api
      .get('contacts')
      .then(({ data }) => {
        setContacts([...contacts, ...data.datas]);
      });
  }, [user.id]);

  const handleClickContact = (id: string) => {
    history.push(`/contacts/${id}/edit`);
  };
  
  const formatDate = (date: Date): string => moment(date).format('DD/MM/yyyy HH:mm');
  
  const handleDeleteContacts = async (event: any) => {
    event.preventDefault();

    try {
      await api.delete('contacts/deleteall');

      setContacts([]);

      addToast({
        type: 'success',
        title: 'Data successfully removed!'
      });
    } catch (err) {
      addToast({
        type: 'error',
        title: 'Unable to remove data from base',
        description: 'An error occurred while trying to remove Contacts from the base, please try again later.',
      });
    }
  }

  return (
    <>
      <Header>
        <HeaderContent>
          <MdOutlineContacts size={24} />
          <strong>Contacts</strong>
        </HeaderContent>
      </Header>

      <SubHeader>
        <Title>Wellcome {user.name}</Title>

        {contacts.length > 0 &&
          <a onClick={handleDeleteContacts}>
            <FiTrash2 size={20}/>
            Delete All
          </a>
        }
      </SubHeader>

      <ContactsContent>
        {contacts &&
            contacts.map(contact => (
                <a key={contact.id} onClick={() => handleClickContact(contact.id)}>
                    <MdOutlineContactMail size={24} />
                    
                    <div>
                        <strong>{contact.name}</strong>
                        {contact.phone && (<p>{contact.phone}</p>)}
                        {contact.whatsapp && (<p>{contact.whatsapp}</p>)}
                        <p>{formatDate(contact.createddate)}</p>
                        <strong>{contact.email}</strong>
                    </div>

                    <FiChevronRight size={20} />
                </a>
            ))
        }
      </ContactsContent>
    </>
  );
}

export default Contacts;
