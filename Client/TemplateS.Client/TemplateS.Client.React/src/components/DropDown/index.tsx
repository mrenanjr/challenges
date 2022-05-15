import React, { useState } from 'react';
import { Dropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';
import { IoIosArrowDown } from 'react-icons/io';
import { GiModernCity } from 'react-icons/gi';

import { Content, DropdownToggleContent } from './styles';

interface IOptions {
    name: string;
    value: string;
}

interface IDropDownProps {
    options: IOptions[];
    isErrored: boolean;
    onChange: Function;
    defaultValue: string | undefined;
}

const DropDown: React.FC<IDropDownProps> = ({
    options,
    isErrored,
    defaultValue,
    onChange
}) => {
    const [dropdownIsOpen, setDropdownIsOpen] = useState(false);
    const [selectValue, setSelectValue] = useState(defaultValue);
    
    return (
        <Content isErrored={isErrored}>
            <Dropdown isOpen={dropdownIsOpen} toggle={() => setDropdownIsOpen(!dropdownIsOpen)}>
                <DropdownToggle className="bg-white text-dark w-100 d-flex align-items-center">
                    <DropdownToggleContent>
                        <GiModernCity />
                            <span>
                                {selectValue || defaultValue ? options.find((elem) => elem.value === (selectValue || defaultValue))?.name : 'Cities'}
                            </span>
                        <IoIosArrowDown />
                    </DropdownToggleContent>
                </DropdownToggle>
                <DropdownMenu className="w-100">
                    {options.map((item) => (
                        <DropdownItem
                            key={`index-${item.value}`}
                            defaultValue={defaultValue}
                            active={selectValue ? item.value === selectValue : item.value === defaultValue }
                            onClick={() => {
                                setSelectValue(item.value);
                                onChange(item.value);
                            }}
                        >
                            {item.name}
                        </DropdownItem>
                    ))}
                </DropdownMenu>
            </Dropdown>
        </Content>
    );
}

export default DropDown;