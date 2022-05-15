/* eslint-disable react-hooks/exhaustive-deps */
/* eslint-disable jsx-a11y/anchor-is-valid */
import React, { useCallback, useEffect, useMemo, useRef, useState } from 'react';
import { FiChevronLeft, FiTrash2 } from 'react-icons/fi';
import { FaCity } from 'react-icons/fa';
import { RiGovernmentLine } from 'react-icons/ri';

import { Header, HeaderContent, SubHeader, SubTitle, Title } from './styles';

import { useAuth } from '../../../hooks/auth';

import api from '../../../services/api';
import { Link, useHistory, useRouteMatch } from 'react-router-dom';
import { FormHandles } from '@unform/core';
import { Form } from '@unform/web';
import * as Yup from 'yup';
import Input from '../../../components/Input';
import Button from '../../../components/Button';
import getValidationErrors from '../../../utils/getValidationErrors';
import { useToast } from '../../../hooks/toast';

interface ICityFormData {
    id: string;
    name: string;
    uf: string;
}

export interface IMatchParams {
  id?: string;
  path: string;
}

const AddOrEditContact: React.FC = () => {
  const match = useRouteMatch<IMatchParams>();
  const history = useHistory();

  const { user } = useAuth();
  const { addToast } = useToast();
  const formRef = useRef<FormHandles>(null);
  const [city, setCity] = useState<ICityFormData>({
    id: '',
    name: '',
    uf: ''
  });
  const isEdit = useMemo(() => {
    var splitPath = match.path.split('/');

    return splitPath[splitPath.length - 1] === 'edit';
  }, [match]);

  const action = isEdit ? 'Edit' : 'Add';

  useEffect(() => {
    if(match.params.id) {
      api
        .get(`cities/${match.params?.id}`)
        .then(({ data }) => {
            setCity({ ...city, ...data.data });
        });
    }
  }, [user.id]);

  const handleSubmit = useCallback(
    async (data: ICityFormData) => {
      try {
        formRef.current?.setErrors({});

        const schema = Yup.object().shape({
            name: Yup.string().required('Name required'),
            uf: Yup.string().required('Name required')
                .max(2, 'Must have 2 characters')
                .min(2, 'Must have 2 characters')
        });

        await schema.validate(data, {
            abortEarly: false,
        });

        data.uf = data.uf.toUpperCase();
        
        if(isEdit) {
            await api.put(`/cities/${data.id}`, data);
        } else {
            await api.post(`/cities`, data);
        }

        addToast({
          type: 'success',
          title: 'Data save successfully!'
        });

        history.push('/cities');
      } catch (err: any) {
        if (err instanceof Yup.ValidationError) {
          formRef.current?.setErrors(getValidationErrors(err));

          return;
        }
  
        let message = err?.response?.data?.error;

        if(!message) message = err?.message;

        addToast({
          type: 'error',
          title: 'Request failed',
          description: message,
        });
      }
    },
    [addToast],
  );
  
  const handleDeleteCity = useCallback(async () => {
    try {
      await api.delete(`/cities/${match.params.id}`);

      addToast({
        type: 'success',
        title: 'Data deleted successfully!'
      });

      history.push('/cities');
    } catch (err: any) {
      let message = err?.response?.data?.error;

      if(!message) message = err?.message;

      addToast({
        type: 'error',
        title: 'Request failed',
        description: message,
      });
    }
  },
  [addToast],
);

  return (
    <>
      <Header>
        <HeaderContent>
          <FaCity size={24} />
          <strong>City</strong>
        </HeaderContent>

        <Link to={'/cities'}>
          <FiChevronLeft size={20} />
          Back
        </Link>
      </Header>

      <SubHeader>
        <Title>Wellcome {user.name}</Title>

        {isEdit &&
          <a onClick={handleDeleteCity}>
            <FiTrash2 size={20}/>
            Delete
          </a>
        }
      </SubHeader>

      <br/>
      <Form ref={formRef} onSubmit={e => handleSubmit({ ...e, id: city.id })}>
        <SubTitle>{action} City</SubTitle>
        <Input name="name" icon={FaCity} defaultValue={city.name} placeholder="Name" />
        <Input name="uf" icon={RiGovernmentLine} defaultValue={city.uf} placeholder="Uf" />
        
        <Button type="submit">Submit</Button>
      </Form>
    </>
  );
}

export default AddOrEditContact;