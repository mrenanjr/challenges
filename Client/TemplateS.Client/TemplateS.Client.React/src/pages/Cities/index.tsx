/* eslint-disable react-hooks/exhaustive-deps */
/* eslint-disable jsx-a11y/anchor-is-valid */
import { useEffect, useState } from 'react';
import { Link, useHistory, useRouteMatch } from 'react-router-dom';
import { FiChevronRight, FiTrash2 } from 'react-icons/fi';
import { IoMdAdd } from 'react-icons/io';
import { FaCity } from 'react-icons/fa';
import { GiModernCity } from 'react-icons/gi';
import moment from 'moment';

import { Header, HeaderContent, CitiesContent, SubHeader, Title } from './styles';

import { useAuth } from '../../hooks/auth';

import api from '../../services/api';
import { useToast } from '../../hooks/toast';

export interface ICity {
    id: string;
    name: string;
    uf: string;
    createddate: Date;
}

const Cities: React.FC = () => {
  const history = useHistory();
  const { user } = useAuth();
  const { url } = useRouteMatch();
  const { addToast } = useToast();

  const [cities, setCities] = useState<ICity[]>([]);

  useEffect(() => {
    api
      .get('cities')
      .then(({ data }) => {
        setCities([...cities, ...data.datas]);
      });
  }, [user.id]);

  const handleClickCity = (id: string) => {
    history.push(`/cities/${id}/edit`);
  };
  
  const formatDate = (date: Date): string => moment(date).format('DD/MM/yyyy HH:mm');
  
  const handleDeleteCities = async (event: any) => {
    event.preventDefault();

    try {
      await api.delete('cities/deleteall');

      setCities([]);

      addToast({
        type: 'success',
        title: 'Data successfully removed!'
      });
    } catch (err) {
      addToast({
        type: 'error',
        title: 'Unable to remove data from base',
        description: 'An error occurred while trying to remove Cities from the base, please try again later.',
      });
    }
  }

  return (
    <>
      <Header>
        <HeaderContent>
          <FaCity size={24} />
          <strong>Cities</strong>
        </HeaderContent>

        <Link to={`${url}/add`}>
          <IoMdAdd size={20} />
          Add City
        </Link>
      </Header>

      <SubHeader>
        <Title>Wellcome {user.name}</Title>

        {cities.length > 0 &&
          <a onClick={handleDeleteCities}>
            <FiTrash2 size={20}/>
            Delete All
          </a>
        }
      </SubHeader>

      <CitiesContent>
        {cities &&
            cities.map(city => (
                <a key={city.id} onClick={() => handleClickCity(city.id)}>
                    <GiModernCity size={24} />
                    
                    <div>
                        <strong>{city.name}</strong>
                        <p>{formatDate(city.createddate)}</p>
                        <strong>{city.uf}</strong>
                    </div>

                    <FiChevronRight size={20} />
                </a>
            ))
        }
      </CitiesContent>
    </>
  );
}

export default Cities;