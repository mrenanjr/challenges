import { Switch } from 'react-router-dom';

import Route from './Route';

import SignIn from '../pages/SingIn';
import Dashboard from '../pages/Dashboard';
import Repository from '../pages/Repository';
import DatabaseRepository from '../pages/DashboardRepository';
import BalancedBrackts from '../pages/BalancedBrackts';
import Users from '../pages/Users';
import Contacts from '../pages/Contacts';
import AddOrEditContact from '../pages/Contacts/AddOrEditContact';
import Persons from '../pages/Persons';
import AddOrEditPerson from '../pages/Persons/AddOrEditPerson';
import Cities from '../pages/Cities';
import AddOrEditCity from '../pages/Cities/AddOrEditCity';

const Routes: React.FC = () => {
    return (
        <Switch>
            <Route path="/" exact component={Dashboard} />
            <Route path="/dashboard" component={Dashboard} />
            <Route path="/signin" component={SignIn} />
            <Route path="/repositories/:repository+" component={Repository} />
            
            <Route path="/balancedbrackets" component={BalancedBrackts} Both />

            <Route path="/databaserepositories" component={DatabaseRepository} isPrivate />
            <Route path="/users" component={Users} isPrivate />

            <Route path="/contacts" exact component={Contacts} isPrivate />
            <Route path="/contacts/add" component={AddOrEditContact} isPrivate />
            <Route path="/contacts/:id/edit" component={AddOrEditContact} isPrivate />

            <Route path="/persons" exact component={Persons} isPrivate />
            <Route path="/persons/add" component={AddOrEditPerson} isPrivate />
            <Route path="/persons/:id/edit" component={AddOrEditPerson} isPrivate />

            <Route path="/cities" exact component={Cities} isPrivate />
            <Route path="/cities/add" component={AddOrEditCity} isPrivate />
            <Route path="/cities/:id/edit" component={AddOrEditCity} isPrivate />
        </Switch>
    );
}

export default Routes;