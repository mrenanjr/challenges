/* eslint-disable react-hooks/exhaustive-deps */
/* eslint-disable jsx-a11y/anchor-is-valid */
import React, { useCallback, useEffect, useMemo, useRef, useState } from 'react';
import { FiMail, FiChevronLeft, FiPhone, FiTrash2 } from 'react-icons/fi';
import { IoIosContact, IoLogoWhatsapp } from 'react-icons/io';

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

interface IContactFormData {
    id: string;
    email?: string;
    phone?: string;
    whatsapp?: string;
}

interface IMatch {
  id?: string;
  path: string;
}

const AddOrEditContact: React.FC = () => {
  const history = useHistory();
  const match = useRouteMatch<IMatch>();
  const { user } = useAuth();
  const { addToast } = useToast();
  const formRef = useRef<FormHandles>(null);
  const [contact, setContact] = useState<IContactFormData>({
    id: '',
    email: '',
    phone: '',
    whatsapp: '',
  });
  const isEdit = useMemo(() => {
    var splitPath = match.path.split('/');

    return splitPath[splitPath.length - 1] === 'edit';
  }, [match]);

  const action = isEdit ? 'Edit' : 'Add';

  useEffect(() => {
    if(match.params.id) {
      api
        .get(`contacts/${match.params?.id}`)
        .then(({ data }) => {
          setContact({ ...contact, ...data.data });
        });
    }
  }, [user.id]);

  const handleSubmit = useCallback(
    async (data: IContactFormData) => {
      try {
        formRef.current?.setErrors({});

        const schema = Yup.object().shape({
            email: Yup.string().required('Email required').email('Enter a valid email address')
        });

        await schema.validate(data, {
            abortEarly: false,
        });

        if(isEdit) {
          await api.put(`/contacts/${data.id}`, data);
        } else {
          await api.post('/contacts', data);
        }

        addToast({
          type: 'success',
          title: 'Data save successfully!'
        });

        history.push('/contacts');
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

  const handleDeleteContact = useCallback(async () => {
      try {
        await api.delete(`/contacts/${match.params.id}`);

        addToast({
          type: 'success',
          title: 'Data deleted successfully!'
        });

        history.push('/contacts');
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
          <strong>Contact</strong>
        </HeaderContent>

        <Link to={'/contacts'}>
          <FiChevronLeft size={20} />
          Back
        </Link>
      </Header>

      <SubHeader>
        <Title>Wellcome {user.name}</Title>

        {isEdit &&
          <a onClick={handleDeleteContact}>
            <FiTrash2 size={20}/>
            Delete
          </a>
        }
      </SubHeader>

      <br/>
      <Form ref={formRef} onSubmit={e => handleSubmit({ ...e, id: contact.id })}>
        <SubTitle>{action} Contact</SubTitle>
        <Input name="email" icon={FiMail} defaultValue={contact.email} placeholder="Email" />
        <Input name="phone" icon={FiPhone} defaultValue={contact.phone} placeholder="Phone" />
        <Input name="whatsapp" icon={IoLogoWhatsapp} defaultValue={contact.whatsapp} placeholder="Whatsapp" />
        
        <Button type="submit">Submit</Button>
      </Form>
    </>
  );
}

export default AddOrEditContact;