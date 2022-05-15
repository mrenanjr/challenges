import React, { useCallback, useRef } from 'react';
import { FiMail, FiLock } from 'react-icons/fi';
import { useHistory } from 'react-router-dom';
import { FormHandles } from '@unform/core';
import { Form } from '@unform/web';
import * as Yup from 'yup';
import { useAuth } from '../../hooks/auth';
import { useToast } from '../../hooks/toast';

import { Header, PRImg, SubHeader, Title, SubTitle } from './styles';

import getValidationErrors from '../../utils/getValidationErrors';
import pullRequestSvg from '../../assets/pr.svg';
import Input from '../../components/Input';
import Button from '../../components/Button';

interface SignInFormData {
    email: string;
    password: string;
  }

const SignIn: React.FC = () => {
    const formRef = useRef<FormHandles>(null);
    
    const { addToast } = useToast();
    const { signIn } = useAuth();

    const history = useHistory();

    const handleSubmit = useCallback(
        async (data: SignInFormData) => {
          try {
            formRef.current?.setErrors({});

            const schema = Yup.object().shape({
                email: Yup.string()
                    .required('Email required')
                    .email('Enter a valid email address'),
                password: Yup.string().required('Password required'),
            });

            await schema.validate(data, {
                abortEarly: false,
            });

            await signIn({
                email: data.email,
                password: data.password,
            });

            history.push('/databaserepositories');
          } catch (err) {
            if (err instanceof Yup.ValidationError) {
                formRef.current?.setErrors(getValidationErrors(err));
      
                return;
              }
      
              addToast({
                type: 'error',
                title: 'Authentication error',
                description:
                  'There was an error logging in, check your credentials.',
              });
          }
        },
        [signIn, addToast, history],
      );

  return (
    <>
      <Header>
        <PRImg>
          <img src={pullRequestSvg}  alt="Pull request SVG"></img>
          <strong>Pull Request Manager</strong>
        </PRImg>
      </Header>

      <SubHeader>
        <Title>Multi Repo PR Manager</Title>
      </SubHeader>

      <br/>
      <Form ref={formRef} onSubmit={handleSubmit}>
        <SubTitle>Log in</SubTitle>
        <Input name="email" icon={FiMail} placeholder="Email" />
        <Input
            name="password"
            icon={FiLock}
            type="password"
            placeholder="Password"
        />
        <Button type="submit">Connect</Button>
      </Form>
    </>
  );
}

export default SignIn;
