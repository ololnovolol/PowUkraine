import React, {useState, useRef} from 'react';
import styled from 'styled-components';
import * as apiService from '../../common/services/apiService'

const SmButton = styled.button`
  background: #88A87D none repeat scroll 0% 0%;
  height: 40px;
  border-color: #fff;
  color: #fff;
  border-radius: 5px;
  align-items: center;
  font-size: 16px;
  &:hover {
    color: #F0A30A;
    background: #6D8764;
    cursor: pointer;
  }
`;

const BgButton = styled.button`
  background: #88A87D none repeat scroll 0% 0%;
  height: 40px;
  border-color: #fff;
  weight: 100%;
  position: relative;
  margin: right 10px;
  color: #F0A30A;
  border-radius: 5px;
  justify-content: space-between;
  font-size: 16px;
  &:hover {
    color: #fff;
    background: #6D8764;
    cursor: pointer;
  }
`;

const Block = styled.div`

  font-size: 1rem;
  position: relative;
  top: 3rem;
  border: 1 rem solid;

`;

const Form = styled.form`
  border-radius: 5px;
  }
`;

export const MessageForm = () => {
  const [file, loadFile] = useState(null);
  const filePicker = useRef(null);

  async function postMsg(values) {

    const data = {
      "Title": values.Title,
      "Description": values.Description,
      "Data": values.Data,
      "PhoneNumber": values.PhoneNumber,
      Attachment: file
    };

    const answer = await apiService.sentMessage(data)
    console.log(answer);
  }

  function addData(val){
    const formData = new FormData();
    formData.append('Attachment',file);
      loadFile(val);
  }

  const handlePick = () => {
    filePicker.current.click();
  }

  

    return (
      <>
            <Form  onSubmit={postMsg}>
            <h1>Create important message</h1> 
                <Block>
                    <label>Title
                    <input name="Title"/>   </label>
                    <label>Phone
                    <input id="telNo" name="PhoneNumber" type="tel" placeholder="+380-99-77-77-777" /></label>
                    <label>Data
                    <input name="Data" type={"date"}/></label>
                    <label>Description
                    <textarea name="Description"/></label> 
                <Block>
                    <label>Add Location</label>
                    <SmButton type='button'>Pin Location</SmButton>
                </Block>

                <Block>
                    <label htmlFor="images">Add File
                    <input className='hidden' type="file" id="images" accept="image/*" ref={filePicker} onChange={addData}/>
                    </label>
                    <SmButton type="button" onClick={handlePick}>PinFile</SmButton>
                </Block>

                <Block>
                    <label>Submit</label>
                    <BgButton type="submit" onClick={postMsg}>Submit</BgButton>
                </Block>

                </Block>
            </Form>

      </>
    );
  };
  
  export default MessageForm;