/* eslint-disable jsx-a11y/anchor-is-valid */
import { useState } from 'react';
import { BiBracket } from 'react-icons/bi';

import { Header, HeaderContent, SubHeader, Title, Form, Error } from './styles';

import api from '../../services/api';
import { useToast } from '../../hooks/toast';

const BalancedBrackts: React.FC = () => {
  const { addToast } = useToast();

  const [input, setInput] = useState('');
  const [message, setMessage] = useState('');

  const handleBalancedBrecketVerification = async (event: any) => {
    event.preventDefault();

    if(!input) {
        setMessage('Inform the balanced bracket to verification');
        return;
    }

    try {
        const { data } = await api.put(`/balancedbrackets/${input}`);

        addToast({
            type: data.message.includes('not') ? 'error' : 'success',
            title: 'Successful verification.',
            description: data.message
        });
    } catch (err: any) {
        let message = err?.response?.data?.error;

        if(!message) message = err?.message;

        addToast({
            type: 'error',
            title: 'Unable to verify.',
            description: message
        });
    }

    setInput('');
    setMessage('');
  }

  return (
    <>
      <Header>
        <HeaderContent>
          <BiBracket size={24} />
          <strong>Balanced Brackets</strong>
        </HeaderContent>
      </Header>

      <SubHeader>
        <Title>Balanced Brackets Verification</Title>
      </SubHeader>

      <Form hasError={!!message} onSubmit={handleBalancedBrecketVerification}>
        <input
          value={input}
          onChange={e => setInput(e.target.value)}
          placeholder={'Enter the balanced bracket'}
        />
        <button type="submit">Verify</button>
      </Form>

      {message && <Error>{message}</Error>}
    </>
  );
}

export default BalancedBrackts;