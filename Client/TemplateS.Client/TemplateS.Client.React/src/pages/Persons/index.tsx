/* eslint-disable react-hooks/exhaustive-deps */
/* eslint-disable jsx-a11y/anchor-is-valid */
import { useEffect, useState } from 'react';
import { Link, useHistory, useRouteMatch } from 'react-router-dom';
import { FiChevronRight, FiTrash2 } from 'react-icons/fi';
import { IoIosContacts, IoIosContact, IoMdAdd } from 'react-icons/io';

import { Header, HeaderContent, PersonsContent, SubHeader, Title } from './styles';

import { useAuth } from '../../hooks/auth';

import api from '../../services/api';
import { IContact } from '../Contacts'
import { ICity } from '../Cities';
import moment from 'moment';
import { useToast } from '../../hooks/toast';

interface IPerson {
    id: string;
    name: string;
    age: number;
    cpf: string;
    city: ICity;
    contacts: IContact[];
    createddate: Date;
}

const Persons: React.FC = () => {
  const history = useHistory();
  const { user } = useAuth();
  const { url } = useRouteMatch();
  const { addToast } = useToast();

  const [persons, setPersons] = useState<IPerson[]>([]);

  useEffect(() => {
    api
      .get('persons')
      .then(({ data }) => {
        setPersons([...persons, ...data.datas]);
      });
  }, [user.id]);

  const handleClickContact = (id: string) => {
    history.push(`/persons/${id}/edit`);
  };
  
  const formatDate = (date: Date): string => moment(date).format('DD/MM/yyyy HH:mm');
  
  const handleDeletePersons = async (event: any) => {
    event.preventDefault();

    try {
      await api.delete('persons/deleteall');

      setPersons([]);

      addToast({
        type: 'success',
        title: 'Data successfully removed!'
      });
    } catch (err) {
      addToast({
        type: 'error',
        title: 'Unable to remove data from base',
        description: 'An error occurred while trying to remove Persons from the base, please try again later.',
      });
    }
  }

  return (
    <>
      <Header>
        <HeaderContent>
          <IoIosContacts size={24} />
          <strong>Persons</strong>
        </HeaderContent>

        <Link to={`${url}/add`}>
          <IoMdAdd size={20} />
          Add Person
        </Link>
      </Header>

      <SubHeader>
        <Title>Wellcome {user.name}</Title>

        {persons.length > 0 &&
          <a onClick={handleDeletePersons}>
            <FiTrash2 size={20}/>
            Delete All
          </a>
        }
      </SubHeader>

      <PersonsContent>
        {persons &&
            persons.map(person => (
                <a key={person.id} onClick={() => handleClickContact(person.id)}>
                    <IoIosContact size={24} />
                    
                    <div>
                        <strong>{person.name} - {person.age}</strong>
                        <p>{formatDate(person.createddate)}</p>
                        <p>{person.cpf}</p>
                        <strong>{person.city.name}</strong>
                    </div>

                    <FiChevronRight size={20} />
                </a>
            ))
        }
      </PersonsContent>
    </>
  );
}

export default Persons;