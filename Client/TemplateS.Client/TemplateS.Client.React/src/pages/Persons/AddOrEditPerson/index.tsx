/* eslint-disable react-hooks/exhaustive-deps */
/* eslint-disable jsx-a11y/anchor-is-valid */
import React, { useCallback, useEffect, useMemo, useRef, useState } from 'react';

import { FiChevronLeft, FiMail, FiPhone, FiTrash2 } from 'react-icons/fi';
import { IoIosContact, IoLogoWhatsapp } from 'react-icons/io';
import { MdOutlinePermContactCalendar } from 'react-icons/md';
import { HiOutlineIdentification } from 'react-icons/hi';

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
import DropDown from '../../../components/DropDown';
import { IMatchParams } from '../../Cities/AddOrEditCity';

interface IPersonFormData {
  id: string;
  name: string;
  cpf: string;
  age?: string;
  cityid: string;
  
  email: string;
  phone?: string;
  whatsapp?: string;
}

interface ICity {
  value: string;
  name: string;
}

const AddOrEditPerson: React.FC = () => {
  const history = useHistory();
  const match = useRouteMatch<IMatchParams>();
  const { user } = useAuth();
  const { addToast } = useToast();
  const formRef = useRef<FormHandles>(null);
  const [person, setPerson] = useState<IPersonFormData>({
    id: '',
    name: '',
    cpf: '',
    age: '',
    cityid: '',
    email: '',
    phone: '',
    whatsapp: ''
  });
  const [cities, setCities] = useState<ICity[]>([]);
  const [cityHasError, setCityHasError] = useState(false);
  
  const isEdit = useMemo(() => {
    var splitPath = match.path.split('/');

    return splitPath[splitPath.length - 1] === 'edit';
  }, [match]);

  const action = isEdit ? 'Edit' : 'Add';

  useEffect(() => {
      if(match.params?.id) {
        api
          .get(`persons/${match.params?.id}`)
          .then(({ data }) => {
            if(data.data) {
              let contact = data.data.contact;
              let city = data.data.city;
              setPerson({
                ...person,
                ...data.data,
                email: contact?.email,
                phone: contact?.phone,
                whatsapp: contact?.whatsapp,
                cityid: city.id
              });
            }
          });
      }
      api
        .get(`cities`)
        .then(({ data }) => {
          var resposneData = data.datas.map((m: any) => ({
            value: m.id,
            name: m.name
          }))
          setCities([ ...cities, ...resposneData ]);
        });
  }, []);

  const handleSubmit = useCallback(
    async (data: IPersonFormData) => {
      try {
        formRef.current?.setErrors({});

        const schema = Yup.object().shape({
            name: Yup.string().required('Email required'),
            cpf: Yup.string().required('Cpf required')
              .max(11, 'Must have 11 characters')
              .min(11, 'Must have 11 characters'),
            age: Yup.string().required('Age required'),
            email: Yup.string().required('Email required').email('Enter a valid email address'),
            cityid: Yup.string().required('City required')
        });

        setCityHasError(false);

        await schema.validate(data, {
            abortEarly: false,
        });

        if(isEdit) {
          await api.put(`/persons/${data.id}`, data);
        } else {
          await api.post('/persons', data);
        }

        addToast({
          type: 'success',
          title: 'Data save successfully!'
        });

        history.push('/persons');
      } catch (err: any) {
        if (err instanceof Yup.ValidationError) {
          let errors = getValidationErrors(err);
          
          formRef.current?.setErrors(errors);
          
          setCityHasError(!!errors['cityid']);

          return;
        }

        setCityHasError(false);

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

  const handleDeletePerson = useCallback(async () => {
    try {
      await api.delete(`/persons/${match.params.id}`);

      addToast({
        type: 'success',
        title: 'Data deleted successfully!'
      });

      history.push('/persons');
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
          <IoIosContact size={24} />
          <strong>Person</strong>
        </HeaderContent>

        <Link to={'/persons'}>
          <FiChevronLeft size={20} />
          Back
        </Link>
      </Header>

      <SubHeader>
        <Title>Wellcome {user.name}</Title>

        {isEdit &&
          <a onClick={handleDeletePerson}>
            <FiTrash2 size={20}/>
            Delete
          </a>
        }
      </SubHeader>

      <br/>
      <Form ref={formRef} onSubmit={e => handleSubmit({ ...e, id: person.id, cityid: person.cityid })}>
        <SubTitle>{action} Person</SubTitle>
        <Input name="name" icon={IoIosContact} defaultValue={person?.name} placeholder="Name" />
        <Input name="age" icon={MdOutlinePermContactCalendar} defaultValue={person?.age} placeholder="Age" />
        <Input name="cpf" icon={HiOutlineIdentification} defaultValue={person?.cpf} placeholder="Cpf" />
        <Input name="email" icon={FiMail} defaultValue={person?.email} placeholder="Email" />
        <Input name="phone" icon={FiPhone} defaultValue={person?.phone} placeholder="Phone" />
        <Input name="whatsapp" icon={IoLogoWhatsapp} defaultValue={person?.whatsapp} placeholder="Whatsapp" />
        <DropDown
          defaultValue={cities.find(f => f.value === person.cityid)?.value}
          options={cities}
          onChange={(value: string) => setPerson({ ...person, cityid: value })}
          isErrored={cityHasError}
        />

        <Button type="submit">Submit</Button>
      </Form>
    </>
  );
}

export default AddOrEditPerson;